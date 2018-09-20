using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace InstagramPageFollowers
{
    class Program
    {
        static void Main(string[] args)
        {
            Excel.Application xl = new Excel.Application();
            xl.Visible = true;

            Excel.Workbook wb = xl.Workbooks.Open(@"C:\Users\Aram\Downloads\Scrap influ Hello Coton - helllo coton.csv");
            Excel.Worksheet worksheet = (Excel.Worksheet)wb.Worksheets["Scrap influ Hello Coton - helll"];
            worksheet.Activate();
            //Excel.Range links = (Excel.Range)worksheet.Range["H3", "H17476"];
            //var linkColumn = worksheet.Columns[3, 7];
            //var followersColumn = worksheet.Columns[3, 8];

            //var proxy = new WebProxy("104.131.214.218:80");
            //var handler = new HttpClientHandler { Proxy = proxy };
            //var client = new HttpClient(handler) { Timeout = TimeSpan.FromSeconds(10) };
            var client = new HttpClient() { Timeout = TimeSpan.FromSeconds(10) };
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36");
            var last = 0;
            var skip = false;
            for (int i = 1736; i <= 17476; i++) // 8094
            {
                try
                {
                    var link = (string)worksheet.Cells[i, 7].Value;
                    if (string.IsNullOrWhiteSpace(link) || link == "https://www.instagram.com" || link == "https://www.instagram.com/" || link == "null") continue;
                    var cell = (worksheet.Cells[i, 8] as Excel.Range)?.Value?.ToString();
                    if (!string.IsNullOrWhiteSpace(cell)) continue;

                    var content = client.GetStringAsync(link).GetAwaiter().GetResult();
                    var count = Regex.Match(content, "\"edge_followed_by\":{\"count\":(\\d*)}");
                    if (count.Success) worksheet.Cells[i, 8].Value = count.Groups[1].Value;
                    Thread.Sleep(1000);
                }
                catch (Exception e)
                {
                    if (e.Message.Contains("404")) continue;
                    if (e.Message.ToLower().Contains("an invalid") || i == last)
                    {
                        File.AppendAllText(@"E:\test\brokenCells.txt", $"{i}{Environment.NewLine}");
                        continue;
                    }

                    Thread.Sleep(5000);
                    last = i;
                    i -= 1;
                    Console.WriteLine(i);
                    Console.WriteLine(e.Message);
                }
            }

            wb.Save();
            wb.Close();
            xl.Save();
            xl.Quit();
        }
    }
}
