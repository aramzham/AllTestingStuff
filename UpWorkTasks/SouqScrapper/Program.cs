using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SouqScrapper
{
    class Program
    {
        private static int count = 0;

        static void Main(string[] args)
        {
            var handler = new HttpClientHandler(){AllowAutoRedirect = false};
            var client = new HttpClient(handler);
            var firstPageUrl = "https://uae.souq.com/ae-en/football/sporting-goods-322%7Cathletic-shoes-534%7Csportswear-467/new/a-t-c/s/?ref=nav&page=1&sortby=sr";
            var firstPageContent = client.GetStringAsync(firstPageUrl).GetAwaiter().GetResult();
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(firstPageContent);
            var itemsFound = doc.DocumentNode.SelectSingleNode(".//li[@class='total']").InnerText.Replace("Items found)", "").Replace(" (", "").Replace(",", "");
            var number = int.Parse(itemsFound) / 60 + 1;

            for (var j = 2; j < number; j++)
            {
                try
                {
                    var content = client.GetStringAsync(firstPageUrl.Replace("page=1", $"page={j}")).GetAwaiter()
                        .GetResult();
                    doc.LoadHtml(content);
                }
                catch (System.Net.Http.HttpRequestException e)
                {
                    if (e.Message.Contains("302 (Moved Temporarily)")) break;
                }
                catch (Exception e)
                {
                    continue;
                }

                var links = doc.DocumentNode.SelectNodes(".//a[contains(@class,'Link')]").Select(x => x.GetAttributeValue("href", "XXX")).Where(x => !string.IsNullOrEmpty(x)).ToList();

                foreach (var link in links)
                {
                    var item = new ItemModel();
                    try
                    {
                        // TODO: what is category, listing date, review count?
                        var itemContent = client.GetStringAsync(link).GetAwaiter().GetResult();
                        doc.LoadHtml(itemContent);
                        item.Link = link;
                        var priceTag = doc.DocumentNode.SelectSingleNode(".//h3[starts-with(@class,'price')]");
                        item.FinalPrice = decimal.Parse(priceTag.InnerText.Trim(' ', '\n', '\t', '\r').Replace("AED", "").Replace("&nbsp;", ""));
                        var ratingTag = doc.DocumentNode
                            .SelectSingleNode(".//div[contains(@class,'product-rating')]//div[contains(@class,'mainRating')]");
                        var ratingScoreTag = ratingTag?.SelectSingleNode(".//strong");
                        var ratingCountTag = ratingTag?.SelectSingleNode(".//div[@class='reviews-total']");
                        item.TotalRatingCount = ratingCountTag is null || !int.TryParse(ratingCountTag.InnerText.Replace("&nbsp;", "").Replace("ratings", "").Replace("rating", ""), out var totalRating) ? 0 : totalRating;
                        if(decimal.TryParse(ratingScoreTag?.InnerText, out var rating)) item.Rating = rating;
                        var headerTag = doc.DocumentNode.SelectSingleNode(".//div[contains(@class,'product-title')]");
                        var titleTag = headerTag.SelectSingleNode(".//h1");
                        item.Title = titleTag is null ? "no title" : titleTag.InnerText;
                        var categoryTag = headerTag.SelectSingleNode(".//span");
                        item.CategoryName = categoryTag.InnerText.Split(new []{", "}, StringSplitOptions.RemoveEmptyEntries).Last().Trim();
                        var dts = doc.DocumentNode.SelectNodes(".//li[@id='specs']//dt");
                        var dds = doc.DocumentNode.SelectNodes(".//li[@id='specs']//dd");
                        for (int i = 0; i < dts.Count; i++)
                        {
                            if (dts[i].InnerText != "Item EAN") continue;
                            item.EAN = dds[i].InnerText;
                            break;
                        }
                        if (item.EAN is null) item.EAN = "no ean";
                        item.ListingDate = DateTime.Now;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                    count++;
                    WriteToCSV(item);
                    Thread.Sleep(1000);
                }
            }
        }

        static void WriteToCSV(ItemModel item)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9:yyyy/MM/dd}{10}", count, item.Link, item.Title, item.ReviewsCount, item.Rating,
                item.TotalRatingCount, item.FinalPrice, item.CategoryName, item.EAN, item.ListingDate, Environment.NewLine);
            File.AppendAllText(@"E:\Aram\Programming\test.csv", sb.ToString());
        }
    }
}
