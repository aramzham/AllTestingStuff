using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;
using Souq.ClassLib.Models;

namespace Souq.ClassLib
{
    public class Parser
    {
        private const string Section2Page1 = "?section=2&page=1";
        private const string Section2Page1WithoutQuestion = "section=2&page=1";
        private const string AllCategoriesLink = "https://uae.souq.com/ae-en/shop-all-categories/c/";
        private const int SleepTime = 1000;

        private static int count = 0;
        private static HttpClientHandler _handler = new HttpClientHandler() { AllowAutoRedirect = false };
        private static HttpClient _client = new HttpClient(_handler);
        private static HtmlDocument _doc = new HtmlDocument();
        private static HashSet<string> _allLinks = new HashSet<string>();
        private static List<LargeCategoryModel> largeCategories;

        public static void Start()
        {
            _client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36");
            if (largeCategories is null) largeCategories = GetLargeCategories();
            if (largeCategories is null) return;
            foreach (var largeCategory in largeCategories)
            {
                Directory.CreateDirectory(largeCategory.Name);
                foreach (var mediumCategory in largeCategory.MediumCategories)
                {
                    Directory.CreateDirectory($"{largeCategory.Name}\\{mediumCategory.Name}");
                    foreach (var smallCategory in mediumCategory.SmallCategories)
                    {
                        var link = $"{smallCategory.Link}{Section2Page1}";
                        var number = GetPageNumber(link) / 60 + 1;
                        GetItems(SleepTime, $"{largeCategory.Name}\\{mediumCategory.Name}\\{smallCategory.Name}.csv", number, link);
                    }
                }

                foreach (var smallCategory in largeCategory.SmallCategories)
                {
                    var link = $"{smallCategory.Link}{Section2Page1}";
                    var number = GetPageNumber(link) / 60 + 1;
                    GetItems(SleepTime, $"{largeCategory.Name}\\{smallCategory.Name}.csv", number, link);
                }
            }
        }

        public static List<LargeCategoryModel> GetLargeCategories()
        {
            try
            {
                var content = _client.GetStringAsync(AllCategoriesLink).GetAwaiter().GetResult();
                _doc.LoadHtml(content);
                var shopAllContainer = _doc.DocumentNode.SelectSingleNode(".//div[@class='row shop-all-container']");
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
                        var largeCategory = new LargeCategoryModel() { Name = HttpUtility.HtmlDecode(h3s[i].InnerText)?.Replace(',', '_').Trim() };
                        var sideNav = groupedLists[i].SelectSingleNode(".//ul[@class='side-nav']");
                        if (sideNav is null) continue;
                        var lis = sideNav.Elements("li");
                        if (lis is null) continue;
                        foreach (var li in lis)
                        {
                            if (li.HasAttributes)
                            {
                                var aParent = li.Element("a");
                                var ulParent = li.Element("ul");
                                if (aParent is null || ulParent is null) continue;
                                var mediumCategory = new MediumCategoryModel() { Name = HttpUtility.HtmlDecode(aParent.InnerText.Replace(',', '_')).Trim() };
                                var lisParent = ulParent.Elements("li");
                                foreach (var liParent in lisParent)
                                {
                                    if (liParent.HasAttributes)
                                    {
                                        var lisParent2 = liParent.SelectNodes(".//ul//li");
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
            catch (Exception e)
            {
                // log
                return null;
            }
        }

        private static SmallCategoryModel GetSmallCategoryFromA(HtmlNode a)
        {
            if (a is null || !a.HasAttributes) return null;
            return new SmallCategoryModel() { Name = HttpUtility.HtmlDecode(a.InnerText.Replace(',', ';')).Trim(), Link = a.GetAttributeValue("href", "no link") };
        }

        private static ItemModel CreateItem(string url)
        {
            var item = new ItemModel();
            var itemContent = _client.GetStringAsync(url).GetAwaiter().GetResult();
            _doc.LoadHtml(itemContent);

            // link
            item.Link = url;

            // final price
            var priceTag = _doc.DocumentNode.SelectSingleNode(".//h3[starts-with(@class,'price')]");
            if (decimal.TryParse(priceTag?.InnerText.Trim(' ', '\n', '\t', '\r').Replace("AED", "").Replace("&nbsp;", ""), out var price)) item.FinalPrice = price;

            // rating and total rating count
            var ratingTag = _doc.DocumentNode.SelectSingleNode(".//div[contains(@class,'product-rating')]//div[contains(@class,'mainRating')]");
            var ratingScoreTag = ratingTag?.SelectSingleNode(".//strong");
            var ratingCountTag = ratingTag?.SelectSingleNode(".//div[@class='reviews-total']");
            if (ratingCountTag != null && int.TryParse(ratingCountTag.InnerText.Replace("&nbsp;", "").Replace("ratings", "").Replace("rating", ""), out var totalRating)) item.TotalRatingCount = totalRating;
            if (decimal.TryParse(ratingScoreTag?.InnerText, out var rating)) item.Rating = rating;

            // category and title
            var headerTag = _doc.DocumentNode.SelectSingleNode(".//div[contains(@class,'product-title')]");
            if (headerTag != null)
            {
                var titleTag = headerTag.SelectSingleNode(".//h1");
                item.Title = titleTag is null ? "no title" : titleTag.InnerText.Replace(',', '.');
                var categoryTag = headerTag.SelectSingleNode(".//span");
                item.CategoryName = categoryTag != null ? categoryTag.InnerText.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries).Last().Trim() : "no category";
            }

            // ean
            var dts = _doc.DocumentNode.SelectNodes(".//li[@id='specs']//dt");
            var dds = _doc.DocumentNode.SelectNodes(".//li[@id='specs']//dd");
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
            var reviewsTag = _doc.DocumentNode.SelectSingleNode(".//a[contains(@data-defaultreviewstab,'REVIEWS') and @class='linkToReviewsTab']");
            if (reviewsTag is null) item.ReviewsCount = 0;
            else
            {
                var reviewsTagText = reviewsTag.InnerText.Replace("reviews", "").Replace("review", "").Replace("&nbsp;", "");
                if (int.TryParse(reviewsTagText, out var reviewCount)) item.ReviewsCount = reviewCount;
            }

            // listing date
            var dateTag = _doc.DocumentNode.SelectSingleNode(".//*[@*[contains(., 'souqcdn.com/item/')]]")?.OuterHtml;
            var date = dateTag?.Split(new[] { "souqcdn.com/item/" }, StringSplitOptions.RemoveEmptyEntries).Last().Take(10);
            if (date != null) item.ListingDate = new string(date.ToArray());

            return item;
        }

        private static void WriteToCsv(ItemModel item, string fileName)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}{10}", count, item.Link, item.Title, item.ReviewsCount, item.Rating,
                item.TotalRatingCount, item.FinalPrice, item.CategoryName, item.EAN, item.ListingDate, Environment.NewLine);
            File.AppendAllText(fileName, sb.ToString());
        }

        private static int GetPageNumber(string firstPageUrl)
        {
            var firstPageContent = _client.GetStringAsync(firstPageUrl).GetAwaiter().GetResult();
            _doc.LoadHtml(firstPageContent);
            var itemsFound = _doc.DocumentNode.SelectSingleNode(".//li[@class='total']").InnerText.Replace("Items found", "").Replace("(", "").Replace(")", "").Replace(",", "").Trim();
            return int.TryParse(itemsFound, out var number) ? number / 60 + 1 : 1;
        }

        private static void GetItems(int sleepTime, string fileName, int pageCount, string url)
        {
            count = 0;
            for (var j = 1; j <= pageCount; j++)
            {
                try
                {
                    var content = _client.GetStringAsync(url.Replace("page=1", $"page={j}")).GetAwaiter().GetResult();
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
                        item = CreateItem(link);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    if (item is null) continue;
                    _allLinks.Add(link);

                    count++;
                    WriteToCsv(item, fileName);
                    Thread.Sleep(new Random().Next(sleepTime - 500, sleepTime + 501));
                }
            }
        }
    }
}
