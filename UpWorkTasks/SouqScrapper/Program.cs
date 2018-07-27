using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;

namespace SouqScrapper
{
    class Program
    {
        private const string Section2Page1 = "?section=2&page=1";
        private const string Section2Page1WithoutQuestion = "section=2&page=1";
        private const string AllCategoriesLink = "https://uae.souq.com/ae-en/shop-all-categories/c/";

        private static int count = 0;
        private static HttpClientHandler _handler = new HttpClientHandler() { AllowAutoRedirect = false };
        private static HttpClient _client = new HttpClient(_handler);
        private static HtmlDocument _doc = new HtmlDocument();
        private static HashSet<string> _allLinks = new HashSet<string>();

        static void Main(string[] args)
        {
            //    var list = new List<string[]>();
            //using (var reader = new StreamReader(@"E:\Aram\Programming\tablet-microsoft.csv"))
            //{
            //    while (!reader.EndOfStream)
            //    {
            //        var line = reader.ReadLine();
            //        var values = line.Split(',');

            //        list.Add(values);
            //    }
            //}
            //var allCategoryLinks = GetAllCategoryLinks(_client, _doc);
            List<LargeCategoryModel> largeCategories = null;
            largeCategories = GetLargeCategories(_client, _doc);

            var firstPageUrl = "https://uae.souq.com/ae-en/tablet/microsoft/new/a-7-c/l/?ref=nav&section=2&page=1";
            var number = GetPageNumber(firstPageUrl, _client, _doc);
            var fileName = GetFileNameFromUrl(firstPageUrl);

            for (var j = 1; j <= number; j++)
            {
                try
                {
                    var content = _client.GetStringAsync(firstPageUrl.Replace("page=1", $"page={j}")).GetAwaiter()
                        .GetResult();
                    _doc.LoadHtml(content);
                }
                catch (System.Net.Http.HttpRequestException e)
                {
                    if (e.Message.Contains("302 (Moved Temporarily)")) break;
                }
                catch (Exception e)
                {
                    continue;
                }

                var links = _doc.DocumentNode.SelectNodes(".//a[contains(@class,'sPrimaryLink')]").Select(x => x.GetAttributeValue("href", "XXX")).Where(x => !string.IsNullOrEmpty(x)).Distinct().ToList();

                foreach (var link in links)
                {
                    if (_allLinks.Contains(link)) continue;
                    ItemModel item = null;
                    try
                    {
                        item = CreateItem(link, _client, _doc);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    if (item is null) continue;
                    _allLinks.Add(link);

                    count++;
                    WriteToCSV(item, fileName);
                    Thread.Sleep(1000);
                }
            }
        }

        private static List<LargeCategoryModel> GetLargeCategories(HttpClient client, HtmlDocument doc)
        {
            var content = client.GetStringAsync(AllCategoriesLink).GetAwaiter().GetResult();
            doc.LoadHtml(content);
            var shopAllContainer = doc.DocumentNode.SelectSingleNode(".//div[@class='row shop-all-container']");
            if (shopAllContainer is null) return null;
            var columns = shopAllContainer.SelectNodes(".//div[@class='large-4 columns']");
            if (columns is null) return null;
            var listOfCategories = new List<LargeCategoryModel>();
            foreach (var column in columns)
            {
                var h3s = column.SelectNodes(".//h3");
                var groupedLists = column.SelectNodes(".//div[@class='grouped-list']");
                if (h3s is null || groupedLists is null || h3s.Count != groupedLists.Count) continue;
                for (int i = 0; i < h3s.Count; i++)
                {
                    var largeCategory = new LargeCategoryModel() { Name = HttpUtility.HtmlDecode(h3s[i].InnerText)?.Replace(',',';').Trim() };
                    var sideNav = groupedLists[i].SelectSingleNode(".//ul[@class='side-nav']");
                    if(sideNav is null) continue;
                    var lis = sideNav.Elements("li");
                    if(lis is null) continue;
                    foreach (var li in lis)
                    {
                        if (li.HasAttributes)
                        {
                            var aParent = li.Element("a");
                            var ulParent = li.Element("ul");
                            if (aParent is null || ulParent is null) continue;
                            var mediumCategory = new MediumCategoryModel() { Name = HttpUtility.HtmlDecode(aParent.InnerText.Replace(',', ';')).Trim() };
                            var lisParent = ulParent.Elements("li");
                            foreach (var liParent in lisParent)
                            {
                                if (liParent.HasAttributes)
                                {
                                    var aParent2 = li.Element("a");
                                    if (aParent2 is null) continue;
                                    var lisParent2 = ulParent.SelectNodes(".//li");
                                    foreach (var liParent2 in lisParent2)
                                    {
                                        var smallCategory = GetSmallCategoryFromA(liParent2.Element("a"));
                                        if (smallCategory != null && smallCategory.Link != "no link") mediumCategory.SmallCategories.Add(smallCategory);
                                    }
                                }
                                else
                                {
                                    var smallCategory = GetSmallCategoryFromA(liParent.Element("a"));
                                    if (smallCategory != null && smallCategory.Link != "no link") mediumCategory.SmallCategories.Add(smallCategory);
                                }
                            }
                            largeCategory.MediumCategories.Add(mediumCategory);
                        }
                        else
                        {
                            var aNonParent = li.Element("a");
                            if (aNonParent is null) continue;
                            var smallCategory = GetSmallCategoryFromA(aNonParent);
                            if (smallCategory != null && smallCategory.Link != "no link") largeCategory.SmallCategories.Add(smallCategory);
                        }
                    }
                    listOfCategories.Add(largeCategory);
                }
            }

            return listOfCategories;
        }

        static SmallCategoryModel GetSmallCategoryFromA(HtmlNode a)
        {
            if (a is null || !a.HasAttributes) return null;
            return new SmallCategoryModel() { Name = HttpUtility.HtmlDecode(a.InnerText.Replace(',',';'))?.Trim(), Link = a.GetAttributeValue("href", "no link") };
        }

        static string GetHrefFromA(HtmlNode a)
        {
            if (a is null) return null;
            var href = a.GetAttributeValue("href", "def");
            return href == "def" ? null : href;
        }

        static HashSet<string> GetAllCategoryLinks(HttpClient client, HtmlDocument doc)
        {
            var links = new HashSet<string>();
            var categoryPageSource = client.GetStringAsync(AllCategoriesLink).GetAwaiter().GetResult();
            doc.LoadHtml(categoryPageSource);
            var groupedLists = doc.DocumentNode.SelectNodes(".//div[@class='grouped-list']");
            if (groupedLists is null) return null;
            foreach (var groupedList in groupedLists)
            {
                var lisInGroup = groupedList.SelectNodes(".//li");
                if (lisInGroup is null) continue;
                foreach (var li in lisInGroup)
                {
                    if (!li.HasClass("parent"))
                    {
                        //continue;
                        var href = GetHrefFromA(li.SelectSingleNode(".//a"));
                        if (href != null) links.Add(href);
                    }
                    else
                    {
                        var subShopListLis = li.SelectNodes(".//ul[@class='sub-shop-list']//li");
                        if (subShopListLis is null) continue;
                        foreach (var subLi in subShopListLis)
                        {
                            var href = GetHrefFromA(subLi.SelectSingleNode(".//a"));
                            if (href != null) links.Add(href);
                        }
                    }
                }
            }

            return links;
        }

        static string GetFileNameFromUrl(string url)
        {
            var newIndex = url.IndexOf("/new");
            return url.Substring(27, newIndex - 27).Replace('/', '-');
        }

        static void WriteToCSV(ItemModel item, string fileName)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}{10}", count, item.Link, item.Title, item.ReviewsCount, item.Rating,
                item.TotalRatingCount, item.FinalPrice, item.CategoryName, item.EAN, item.ListingDate, Environment.NewLine);
            File.AppendAllText($@"E:\Aram\Programming\{fileName}.csv", sb.ToString());
        }

        static int GetPageNumber(string firstPageUrl, HttpClient client, HtmlDocument doc)
        {
            var firstPageContent = client.GetStringAsync(firstPageUrl).GetAwaiter().GetResult();
            doc.LoadHtml(firstPageContent);
            var itemsFound = doc.DocumentNode.SelectSingleNode(".//li[@class='total']").InnerText.Replace("Items found", "").Replace("(", "").Replace(")", "").Replace(",", "").Trim();
            var number = int.Parse(itemsFound) / 60 + 1;
            return number;
        }

        static ItemModel CreateItem(string url, HttpClient client, HtmlDocument doc)
        {
            var item = new ItemModel();
            var itemContent = client.GetStringAsync(url).GetAwaiter().GetResult();
            doc.LoadHtml(itemContent);

            // link
            item.Link = url;

            // final price
            var priceTag = doc.DocumentNode.SelectSingleNode(".//h3[starts-with(@class,'price')]");
            if (decimal.TryParse(priceTag?.InnerText.Trim(' ', '\n', '\t', '\r').Replace("AED", "").Replace("&nbsp;", ""), out var price)) item.FinalPrice = price;

            // rating and total rating count
            var ratingTag = doc.DocumentNode.SelectSingleNode(".//div[contains(@class,'product-rating')]//div[contains(@class,'mainRating')]");
            var ratingScoreTag = ratingTag?.SelectSingleNode(".//strong");
            var ratingCountTag = ratingTag?.SelectSingleNode(".//div[@class='reviews-total']");
            if (ratingCountTag != null && int.TryParse(ratingCountTag.InnerText.Replace("&nbsp;", "").Replace("ratings", "").Replace("rating", ""), out var totalRating)) item.TotalRatingCount = totalRating;
            if (decimal.TryParse(ratingScoreTag?.InnerText, out var rating)) item.Rating = rating;

            // category and title
            var headerTag = doc.DocumentNode.SelectSingleNode(".//div[contains(@class,'product-title')]");
            if (headerTag != null)
            {
                var titleTag = headerTag.SelectSingleNode(".//h1");
                item.Title = titleTag is null ? "no title" : titleTag.InnerText.Replace(',', '.');
                var categoryTag = headerTag.SelectSingleNode(".//span");
                item.CategoryName = categoryTag != null ? categoryTag.InnerText.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries).Last().Trim() : "no category";
            }

            // ean
            var dts = doc.DocumentNode.SelectNodes(".//li[@id='specs']//dt");
            var dds = doc.DocumentNode.SelectNodes(".//li[@id='specs']//dd");
            if (dts is null || dds is null) item.EAN = "no ean";
            else
            {
                for (int i = 0; i < dts.Count; i++)
                {
                    if (dts[i].InnerText != "Item EAN") continue;
                    item.EAN = dds[i].InnerText;
                    break;
                }
            }

            // review count
            var reviewsTag = doc.DocumentNode.SelectSingleNode(".//a[contains(@data-defaultreviewstab,'REVIEWS') and @class='linkToReviewsTab']");
            if (reviewsTag is null) item.ReviewsCount = 0;
            else
            {
                var reviewsTagText = reviewsTag.InnerText.Replace("reviews", "").Replace("review", "").Replace("&nbsp;", "");
                if (int.TryParse(reviewsTagText, out var reviewCount)) item.ReviewsCount = reviewCount;
            }

            // listing date
            var dateTag = doc.DocumentNode.SelectSingleNode(".//*[@*[contains(., 'souqcdn.com/item/')]]")?.OuterHtml;
            var date = dateTag?.Split(new[] { "souqcdn.com/item/" }, StringSplitOptions.RemoveEmptyEntries).Last().Take(10);
            if (date != null) item.ListingDate = new string(date.ToArray());

            return item;
        }
    }
}
