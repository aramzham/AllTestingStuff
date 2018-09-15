using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using ClosedXML.Excel;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace FoodPanda
{
    class Program
    {
        private const string MainUrl = "https://www.foodpanda.sg";

        static void Main(string[] args)
        {
            var list = new List<RestaurantModel>();
            var doc = new HtmlDocument();
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36");
            var s = client.GetStringAsync("https://www.foodpanda.sg/city/singapore").GetAwaiter().GetResult();
            doc.LoadHtml(s);
            var vendorList = doc.DocumentNode.SelectSingleNode(".//ul[starts-with(@class,'vendor-list')]");
            var lis = vendorList.SelectNodes("./li");
            var links = lis.Select(x => $"{MainUrl}{x.SelectSingleNode(".//a[@data-flood-closed-message]").GetAttributeValue("href", "")}").ToList();
            foreach (var link in links)
            {
                var content = string.Empty;
                try
                {
                    content = client.GetStringAsync(link).GetAwaiter().GetResult();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                if (string.IsNullOrEmpty(content)) continue;
                doc = new HtmlDocument();
                doc.LoadHtml(content);
                var divInfos = doc.DocumentNode.SelectSingleNode(".//div[@class='infos']");

                var restaurant = new RestaurantModel();
                restaurant.CompanyName = ToNormalString(divInfos.SelectSingleNode(".//h1[@class='vendor-name']").InnerText);
                restaurant.ShortDescription = ClearWhitespaces(divInfos.SelectSingleNode(".//ul[@class='vendor-cuisines']").InnerText);

                var panel = doc.DocumentNode.SelectSingleNode(".//div[@class='panel']");
                restaurant.Address = ToNormalString(panel.SelectSingleNode(".//p[@class='vendor-location']").InnerText);
                restaurant.Url = link;
                restaurant.DeliveryHours = ClearWhitespaces(panel.SelectSingleNode(".//ul[@class='vendor-delivery-times']").InnerText);
                restaurant.OtherInfo = "no other info";

                list.Add(restaurant);
                Thread.Sleep(2000);
                content = string.Empty;
            }

            WriteToExcel(list);
        }

        static void WriteToFile(RestaurantModel rest)
        {
            File.AppendAllText(@"E:\test\foodpanda.txt", $"{rest.CompanyName}, {rest.ShortDescription}, {rest.Address}, {rest.Url}, {rest.DeliveryHours}, {rest.OtherInfo}{Environment.NewLine}");
        }

        static void WriteToExcel(List<RestaurantModel> rests)
        {
            Microsoft.Office.Interop.Excel.Application oXL;
            Microsoft.Office.Interop.Excel._Workbook oWB;
            Microsoft.Office.Interop.Excel._Worksheet oSheet;
            Microsoft.Office.Interop.Excel.Range oRng;
            object misvalue = System.Reflection.Missing.Value;

            oXL = new Microsoft.Office.Interop.Excel.Application();
            oXL.Visible = true;

            //Get a new workbook.
            oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add("FoodPanda"));
            oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;

            //Add table headers going cell by cell.
            oSheet.Cells[1, 1] = "Company Name";
            oSheet.Cells[1, 2] = "Short Description";
            oSheet.Cells[1, 3] = "Address";
            oSheet.Cells[1, 4] = "Url";
            oSheet.Cells[1, 5] = "Delivery Hours";
            oSheet.Cells[1, 6] = "Other Info";

            for (int i = 0; i < rests.Count; i++)
            {
                oSheet.Cells[i + 2, 1] = rests[i].CompanyName;
                oSheet.Cells[i + 2, 2] = rests[i].ShortDescription;
                oSheet.Cells[i + 2, 3] = rests[i].Address;
                oSheet.Cells[i + 2, 4] = rests[i].Url;
                oSheet.Cells[i + 2, 5] = rests[i].DeliveryHours;
                oSheet.Cells[i + 2, 6] = rests[i].OtherInfo;
            }

            oWB.Save();
            oWB.Close();
        }

        static string ClearWhitespaces(string s)
        {
            return string.Join(" / ", s.Split('\r', '\n').Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => Regex.Replace(ToNormalString(x), "[\r,\n,\t, ]{2,}", " / ", RegexOptions.Singleline)));
        }

        static string ToNormalString(string s)
        {
            return s is null ? null : WebUtility.HtmlDecode(s.Trim(' ', '\r', '\n', '\t'));
        }
    }

    public class RestaurantModel
    {
        public string CompanyName { get; set; }
        public string ShortDescription { get; set; }
        public string Address { get; set; }
        public string Url { get; set; }
        public string DeliveryHours { get; set; }
        public string OtherInfo { get; set; }
    }
}
