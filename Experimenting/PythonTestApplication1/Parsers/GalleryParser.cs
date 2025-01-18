using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace ParsersLib.Parsers
{
    public class GalleryParser
    {
        private const string MainUrl = "http://www.gallery.am/hy/database/?d_l={origin}&d_s={section}&d_a=0&kyw=&p={page}&s_b=0";
        private const string BaseUrl = "http://www.gallery.am";
        private const string BasePath = @"E:\Gallery";
        private List<string> _origins = new List<string>() { "All", "Armenian", "Russian", "foreign", "diaspora", "ecclesiastical", "Middle East", "Far East", "unknown" };
        private List<string> _sections = new List<string>() { "All", "Painting", "Drawing", "Engraving", "Sculpture", "Decorative-Applied Art" };

        private WebClient _webClient;

        public GalleryParser()
        {
            _webClient = new WebClient();
            _webClient.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.100 Safari/537.36");
            _webClient.Headers.Add("Upgrade-Insecure-Requests", "1");
            _webClient.Headers.Add("Host", "www.gallery.am");
            _webClient.Headers.Add("DNT", "1");
            _webClient.Headers.Add("Pragma", "no-cache");
        }

        public void DownloadPictures()
        {
            for (int i = 1; i < _origins.Count; i++)
            {
                for (int j = 1; j < _sections.Count; j++)
                {
                    var detailsPath = $@"{BasePath}\{_origins[i]}\{_sections[j]}\details.txt";
                    if (!File.Exists(detailsPath))
                        continue;

                    var lines = File.ReadAllLines(detailsPath);
                    var counter = 1;
                    for (var index = 0; index < lines.Length; index++)
                    {
                        var line = lines[index];
                        var splitBySemicolon = line.Split(';');
                        var pictureLink = splitBySemicolon[2];
                        var author = splitBySemicolon[0].Replace('"', '\'').Replace('?', 'X').Replace(':', '-')
                            .Replace('/', '-');
                        var title = splitBySemicolon[1].Replace('"', '\'').Replace('?', 'X').Replace(':', '-')
                            .Replace('/', '-');

                        var authorFolder = $@"{BasePath}/{_origins[i]}/{_sections[j]}/{author}";
                        if (!Directory.Exists(authorFolder))
                            Directory.CreateDirectory(authorFolder);

                        try
                        {
                            _webClient.DownloadFile(pictureLink,
                                $@"{authorFolder}/{title}.jpg");
                        }
                        catch (Exception e)
                        {
                            File.AppendAllText($@"{BasePath}/{_origins[i]}/{_sections[j]}/exceptions.txt",
                                $"{index}. author = {author}, title = {title}, exception = {e}{Environment.NewLine}");
                        }

                        Console.WriteLine($"{counter}. {_origins[i]} {_sections[j]}");
                        counter++;
                    }
                }
            }
        }

        /// <summary>
        /// 2
        /// </summary>
        public void SetDetails()
        {
            for (int i = 1; i < _origins.Count; i++)
            {
                for (int j = 1; j < _sections.Count; j++)
                {
                    var linksPath = $@"{BasePath}\{_origins[i]}\{_sections[j]}\links.txt";
                    if (!File.Exists(linksPath))
                        continue;

                    var links = File.ReadAllLines(linksPath);

                    var counter = 1;
                    foreach (var link in links)
                    {
                        _webClient.Encoding = Encoding.UTF8;
                        var html = _webClient.DownloadString(link);
                        var doc = new HtmlDocument();
                        doc.LoadHtml(WebUtility.HtmlDecode(html));

                        var col1 = doc.DocumentNode.SelectSingleNode(".//div[@id='item-col1']");
                        var col2 = doc.DocumentNode.SelectSingleNode(".//div[@id='item-col2']");
                        var col3 = doc.DocumentNode.SelectSingleNode(".//div[@id='item-col3']");

                        var authorName = col1.SelectSingleNode(".//h2")?.InnerText.Trim(' ', '\n', '\t', '\r');
                        var imageLink = col2.SelectSingleNode(".//img")?.GetAttributeValue("src", "no link");
                        var material = string.Empty;
                        var size = string.Empty;
                        var additionalInfo = string.Empty;
                        var title = string.Empty;
                        var descriptionPs = col3.SelectNodes(".//p");
                        if (descriptionPs != null && descriptionPs.Count > 0)
                        {
                            title = descriptionPs[0]?.InnerText.Trim(' ', '\n', '\t', '\r');
                            if (descriptionPs.Count > 1)
                            {
                                var materialAndSize = descriptionPs[1]?.InnerHtml.Split(new[] { "<br>" }, StringSplitOptions.RemoveEmptyEntries);
                                if (materialAndSize.Length > 1)
                                    size = materialAndSize[1];

                                material = materialAndSize[0];
                            }
                            if (descriptionPs.Count > 3)
                                additionalInfo = descriptionPs[3]?.InnerText.Trim(' ', '\n', '\t', '\r');
                        }

                        File.AppendAllText($@"{BasePath}\{_origins[i]}\{_sections[j]}\details.txt", $"{authorName};{title};{BaseUrl}{imageLink};{material};{size};{additionalInfo}{Environment.NewLine}");

                        Console.WriteLine($"{counter}. {authorName};{title}");
                        counter++;
                    }
                }
            }
        }

        /// <summary>
        /// 1
        /// </summary>
        public void SetLinks()
        {
            for (int i = 1; i < _origins.Count; i++)
            {
                var originPath = $@"{BasePath}\{_origins[i]}";
                if (!Directory.Exists(originPath))
                    Directory.CreateDirectory(originPath);

                for (int j = 1; j < _sections.Count; j++)
                {
                    var sectionPath = $@"{originPath}\{_sections[j]}";
                    if (!Directory.Exists(sectionPath))
                        Directory.CreateDirectory(sectionPath);

                    var counter = 1;
                    while (true)
                    {
                        var html = _webClient.DownloadString(MainUrl.Replace("{origin}", i.ToString())
                            .Replace("{section}", j.ToString()).Replace("{page}", counter.ToString()));
                        var doc = new HtmlDocument();
                        doc.LoadHtml(html);
                        var dbImages = doc.DocumentNode.SelectNodes(".//div[@class='db-images']");
                        if (dbImages is null || !dbImages.Any())
                            break;

                        foreach (var dbImage in dbImages)
                        {
                            var a = dbImage.SelectSingleNode(".//a[@href]");
                            if (a is null)
                                continue;

                            var pictureLink = a.GetAttributeValue("href", "no link");
                            File.AppendAllText($@"{sectionPath}\links.txt", $"{BaseUrl}{pictureLink}{Environment.NewLine}");
                        }
                        //Console.WriteLine($"i={_origins[i]}, j={_sections[j]}, counter={counter}");
                        counter++;
                    }
                }
            }
        }
    }
}