using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace TestsOfAllKinds
{
    public class BetAtHomeParser
    {
        private static readonly string _mainUrl = "https://www.bet-at-home.com/svc/livebet/data?lang=EN&jid=1&_={timestamp}";
        private static readonly DateTime _epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private static Regex _hdpRegex = new Regex(@"\((\+|-)(\d+\.*\d*)", RegexOptions.Compiled);

        private async Task<string> GetJObject()
        {
            var random = new Random();
            var handler = new HttpClientHandler() { Proxy = new WebProxy("75.146.218.153", 55768) };
            var client = new HttpClient(handler);
            client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36");
            while (true)
            {
                var timestamp = (int)((DateTime.Now - _epoch).TotalSeconds);
                var json = await client.GetStringAsync(_mainUrl.Replace("{timestamp}", timestamp.ToString()));
                if (!string.IsNullOrWhiteSpace(json)) return json;
                Thread.Sleep(random.Next(800, 1201));
            }
        }

        public BookmakerModelNew Parse()
        {
            var bookmaker = new BookmakerModelNew();

            try
            {
                var content = GetJObject().GetAwaiter().GetResult();
                var lines = content.Split('\n').Select(x => x.Split('|')).ToList();
                //var sports = lines.Where(x=>x[0].StartsWith("S")).ToDictionary(k=>k[1], v=>v[3]);
                //var leagues = lines.Where(x => x[0].StartsWith("G")).ToDictionary(k => k[1], v => v);
                var matchExample = lines.FirstOrDefault(x => x[0].StartsWith("E"));
                var matches = lines.Where(x => x[0].StartsWith("E")).ToDictionary(k => k[1], v => new MatchModelNew()
                {
                    StartTime = int.TryParse(v[2], out var ts) ? _epoch.AddSeconds(ts) : default(DateTime?),
                    SportName = v[11],
                    RegionName = v[12],
                    CompetitionName = v[13],
                    MatchMembers = new List<MatchMemberModelNew>()
                    {
                        new MatchMemberModelNew() {Name = v[15], IsHome = true},
                        new MatchMemberModelNew() {Name = v[16], IsHome = false}
                    }
                });
                var marketExample = lines.Where(x => x[0].StartsWith("B")).Take(10).ToList();
                var markets = lines.Where(x => x[0].StartsWith("B")).ToDictionary(k => k[2], v => new MarketModel()
                {
                    Name =v[3],
                    MHandicap = v[14].Length >1 && v[14][2] == '+' && double.TryParse(v[15], out var hdp) 
                        ? hdp * -1 
                        : double.TryParse(v[15], out var hcap) 
                            ? hcap 
                            : default(double?),
                    Selections = new List<SelectionModel>()
                    {
                        new SelectionModel()
                        {
                            Name = v[24],
                            Price = decimal.TryParse(v[6], out var p1) ? p1 : -1,
                            HandicapSign = v[14].Contains("+") && v[24].Contains("+") ? +1 : v[14].Contains("+") && v[24].Contains("-") ? -1 : default(int?)
                        },
                        new SelectionModel()
                        {
                            Name = v[25],
                            Price = decimal.TryParse(v[7], out var p2) ? p2 : -1
                        },
                        new SelectionModel()
                        {
                            Name = v[26],
                            Price = decimal.TryParse(v[8], out var p3) ? p3 : -1,
                            HandicapSign = v[14].Contains("+") && v[26].Contains("+") ? +1 : v[14].Contains("+") && v[26].Contains("-") ? -1 : default(int?)
                        },
                    }
                });
                var selections = lines.Where(x => x[0].StartsWith("P")).ToDictionary(k => k[3], v => v);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return bookmaker;
        }
    }
}