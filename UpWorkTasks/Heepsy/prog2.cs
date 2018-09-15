//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading;
//using System.Threading.Tasks;
//using HtmlAgilityPack;
//using Newtonsoft.Json;

//namespace Heepsy
//{
//    class Program
//    {
//        static string url = "https://www.heepsy.com/influencers?utf8=%E2%9C%93&filter%5Bfixed_search%5D=&filter%5Blocation_aal0%5D=France&filter%5Blocation_aal1%5D=&filter%5Blocation_type%5D=mixed_frequent_location&filter%5Bcategories_AND%5D%5B%5D=&filter%5Bmentions_AND_mobile%5D=&filter%5Bmentions_AND%5D%5B%5D=&filter%5Binstagram_followers_greater_than%5D=&filter%5Binstagram_followers_less_than%5D=&filter%5Binstagram_engagement_greater_than%5D=&filter%5Binstagram_engagement_less_than%5D=&filter%5Binstagram_emv_greater_than%5D=&filter%5Binstagram_emv_less_than%5D=&filter%5Border_by_property%5D=&page={pageNumber}";
//        static HtmlDocument _doc = new HtmlDocument();
//        static void Main(string[] args)
//        {
//            var client = new WebClient();
//            client.Headers.Add(HttpRequestHeader.Cookie, "_ga=GA1.2.404462484.1536688315; intercom-id-t9epzkvq=3639cc34-bbb4-4111-acfd-55b33bef7d88; grsf.cid=vz6syr; grsf.ccid=aeobcm; grsf.cccid=aeobcmvz6syr; intercom-lou-t9epzkvq=1; _gid=GA1.2.1214498973.1536781221; fs_uid=fullstory.com`84XC9`5930361675579392:5686306919153664`23351`; fs_intercom=5930361675579392:5686306919153664; session_cookie=6632f761947019e75b19d89407dcac94; intercom-session-t9epzkvq=UDFsVUdUeGQxNERFK1FqbFNNYmROL2NqUmtRS0lwaXRKL0JreU9SQ0Q3ZEZ6OHlDUGxjbHVIRHpiajI1ZFRGdy0tRjZLTXZlSTRrbGV5VWpiTG16MEhuZz09--ee9f85698845a3eea993906a6901ef5d7f647a56; _gat=1");
//            client.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36");
//            client.Headers.Add(HttpRequestHeader.Accept, "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
//            client.Headers.Add(HttpRequestHeader.Referer, "https://www.heepsy.com/login");
//            client.Headers.Add("Upgrade-Insecure-Requests", "1");
//            //client.Headers.Add(HttpRequestHeader.Connection, "keep-alive");

//            var proxy = new WebProxy("176.113.23.178:40305");
//            for (int i = 0; i < 4070; i++) //125
//            {
//                try
//                {
//                    //client.Proxy = proxy;
//                    if (i != 0) client.Headers.Add(HttpRequestHeader.Referer, url.Replace("{pageNumber}", (i - 1).ToString()));
//                    var downloadString = client.DownloadString(url.Replace("{pageNumber}", i.ToString()));
//                    var jsons = GetJsonStrings(downloadString);
//                    //if (downloadString.Contains("Continue Browsing"))
//                    //{
//                    //    SendMail("aram532@yandex.ru");
//                    //    downloadString = client.DownloadString(url.Replace("{pageNumber}", i.ToString()));
//                    //    jsons = GetJsonStrings(downloadString);
//                    //}
//                    //if (i != 0 && i % 10 == 0)
//                    //{
//                    //    client.DownloadString("https://www.heepsy.com/influencers");
//                    //    Thread.Sleep(5000);
//                    //}

//                    foreach (var json in jsons)
//                    {
//                        try
//                        {
//                            var model = GetModel(json);
//                            WriteToFile(model);
//                        }
//                        catch (Exception e)
//                        {
//                            Console.WriteLine(e);
//                        }
//                    }
//                }
//                catch (Exception e)
//                {
//                    Console.WriteLine(e);
//                }
//                finally
//                {
//                    Thread.Sleep(3000);
//                }
//            }
//        }

//        static List<string> GetJsonStrings(string html)
//        {
//            _doc.LoadHtml(html);
//            var scripts = _doc.DocumentNode.SelectNodes(".//script");
//            if (scripts is null) return null;
//            var scriptsWithVariables = scripts.Where(x => x.InnerText.Contains("var influencer_"));
//            var list = new List<string>();
//            foreach (var node in scriptsWithVariables)
//            {
//                var match = Regex.Match(node.InnerText, "var influencer_(\\d*) = (.*?)var initializeCard_", RegexOptions.Singleline);
//                if (match.Success) list.Add(match.Groups[2].Value.TrimEnd(';', ' ', '\r', '\n', '\t'));
//            }

//            return list;
//        }

//        static HeepsyModel GetModel(string str)
//        {
//            var influencer = JsonConvert.DeserializeObject<Influencer>(str);
//            if (influencer is null) return null;
//            var heepsy = new HeepsyModel();
//            heepsy.InstagramName = $"@{influencer.profileInfo?.username}";
//            heepsy.Engagement = $"{influencer.engagement?.percent} %";
//            heepsy.NumberOfFollowers = influencer.metrics?.follower_count;
//            heepsy.InstagramUrl = influencer.externalLinks?.instagram;
//            heepsy.Name = influencer.profileInfo?.full_name;
//            heepsy.Email = string.Join(" ", influencer.personalInfo.envelopeo);

//            return heepsy;
//        }

//        public static void WriteToFile(HeepsyModel model)
//        {
//            File.AppendAllText(@"E:\test\heepsy.txt", $"{model.InstagramName}, {model.Name}, {model.Email}, {model.NumberOfFollowers}, {model.InstagramUrl}, {model.Engagement}{Environment.NewLine}");
//        }

//        //public async Task SendMail(string email)
//        //{
//        //    var msg = new MailMessage { Body = "Open heepsy.com and check", IsBodyHtml = false, Subject = "Tell that you are not a robot" };
//        //    msg.To.Add(email);
//        //    var sc = new SmtpClient();
//        //    sc.Send(msg);
//        //}
//    }

//    public class HeepsyModel
//    {
//        public string InstagramName { get; set; }
//        public string Name { get; set; }
//        public string Email { get; set; }
//        public string NumberOfFollowers { get; set; }
//        public string InstagramUrl { get; set; }
//        public string Engagement { get; set; }
//    }

//    public class Influencer
//    {
//        public Profileinfo profileInfo { get; set; }
//        public Externallinks externalLinks { get; set; }
//        public Disabledlinks disabledLinks { get; set; }
//        public Personalinfo personalInfo { get; set; }
//        public Metrics metrics { get; set; }
//        public Cost cost { get; set; }
//        public Mentions mentions { get; set; }
//        public Locationscoordsnames locationsCoordsNames { get; set; }
//        public string[] growthChartLabel { get; set; }
//        public int[] growthChartData { get; set; }
//        public Hashtags hashtags { get; set; }
//        public string[] categories { get; set; }
//        public Engagement engagement { get; set; }
//        public List_Info list_info { get; set; }
//    }

//    public class Profileinfo
//    {
//        public string bio { get; set; }
//        public string username { get; set; }
//        public string avatar { get; set; }
//        public string full_name { get; set; }
//        public string[] last_posts { get; set; }
//        public bool v { get; set; }
//        public object note { get; set; }
//        public object rating { get; set; }
//    }

//    public class Externallinks
//    {
//        public string instagram { get; set; }
//        public string[] links { get; set; }
//    }

//    public class Disabledlinks
//    {
//        public string envelopeo { get; set; }
//        public string[] links { get; set; }
//    }

//    public class Personalinfo
//    {
//        [JsonProperty("envelope-o")]
//        public string[] envelopeo { get; set; }
//    }

//    public class Metrics
//    {
//        public string follower_count { get; set; }
//        public string following_count { get; set; }
//        public string likes { get; set; }
//        public object last_month_growth { get; set; }
//        public string avg_likes { get; set; }
//        public string avg_likes_percent { get; set; }
//        public string avg_video_views { get; set; }
//        public string avg_v_views_percent { get; set; }
//        public string avg_comments { get; set; }
//        public string avg_com_percent { get; set; }
//    }

//    public class Cost
//    {
//        public string peemv { get; set; }
//        public string veemv { get; set; }
//    }

//    public class Mentions
//    {
//        public _24Sevres _24sevres { get; set; }
//        public Flavieaudi flavieaudi { get; set; }
//        public Wallpapermag wallpapermag { get; set; }
//        public Lebonmarcherivegauche lebonmarcherivegauche { get; set; }
//        public Theofficialselfridges theofficialselfridges { get; set; }
//        public Lafayetteanticipations lafayetteanticipations { get; set; }
//    }

//    public class _24Sevres
//    {
//        public string fc { get; set; }
//        public string fn { get; set; }
//        public string[] ms { get; set; }
//        public string pp { get; set; }
//        public int fws { get; set; }
//    }

//    public class Flavieaudi
//    {
//        public string fc { get; set; }
//        public string fn { get; set; }
//        public string[] ms { get; set; }
//        public string pp { get; set; }
//        public int fws { get; set; }
//    }

//    public class Wallpapermag
//    {
//        public string fc { get; set; }
//        public string fn { get; set; }
//        public string[] ms { get; set; }
//        public string pp { get; set; }
//        public int fws { get; set; }
//    }

//    public class Lebonmarcherivegauche
//    {
//        public string fc { get; set; }
//        public string fn { get; set; }
//        public string[] ms { get; set; }
//        public string pp { get; set; }
//        public int fws { get; set; }
//    }

//    public class Theofficialselfridges
//    {
//        public string fc { get; set; }
//        public string fn { get; set; }
//        public string[] ms { get; set; }
//        public string pp { get; set; }
//        public int fws { get; set; }
//    }

//    public class Lafayetteanticipations
//    {
//        public string fc { get; set; }
//        public string fn { get; set; }
//        public string[] ms { get; set; }
//        public string pp { get; set; }
//        public int fws { get; set; }
//    }

//    public class Locationscoordsnames
//    {
//        public Frequent frequent { get; set; }
//        public Recent recent { get; set; }
//    }

//    public class Frequent
//    {
//        public float[] coords { get; set; }
//        public string name { get; set; }
//    }

//    public class Recent
//    {
//        public float[] coords { get; set; }
//        public string name { get; set; }
//    }

//    public class Hashtags
//    {
//        public Array[] array { get; set; }
//        public int total { get; set; }
//    }

//    public class Array
//    {
//        public string text { get; set; }
//        public int count { get; set; }
//    }

//    public class Engagement
//    {
//        public string level { get; set; }
//        public int percent { get; set; }
//        public object progress { get; set; }
//        public string wording { get; set; }
//    }

//    public class List_Info
//    {
//        public int id { get; set; }
//        public string avatar { get; set; }
//    }
//}
