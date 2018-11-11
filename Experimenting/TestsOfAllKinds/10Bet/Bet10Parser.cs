using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TestsOfAllKinds._10Bet
{
    public class Bet10Parser
    {
        private List<string> _proxies = new List<string>() { "104.168.157.236:3128", "69.10.47.1:8080", "24.217.192.131:42984", "216.176.143.240:32556", "38.98.171.2:60490"};
        private int _index = 0;

        private string GetData()
        {
            var random = new Random();
            try
            {
                var proxy = new WebProxy(_proxies[_index % 5]);
                var handler = new HttpClientHandler() { Proxy = proxy };
                var client = new HttpClient(handler) { Timeout = TimeSpan.FromSeconds(10) };
                while (true)
                {
                    client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.77 Safari/537.36");
                    client.DefaultRequestHeaders.Add("referer", "https://www.10bet.com/live-betting/");
                    client.DefaultRequestHeaders.Add("requesttarget", "AJAXService");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                    var response = client.PostAsync("https://www.10bet.com/pagemethods_ros.aspx/UpdateEvents", new StringContent("31876472%2339")).GetAwaiter().GetResult();
                    var a = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    if (!string.IsNullOrEmpty(a))
                        return a;

                    Thread.Sleep(random.Next(1000, 2001));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _index++;
            }

            return null;
        }

        public BookmakerModelNew Parse()
        {
            var bookmaker = new BookmakerModelNew();
            try
            {
                var content = GetData();
                if (string.IsNullOrEmpty(content))
                    return null;

                var arr1 = JArray.Parse(content);
                if (arr1 is null || arr1.Count == 0)
                    return null;
                
                foreach (var item1 in arr1[0])
                {
                    if (!(item1 is JArray))
                        continue;

                    var match = new MatchModelNew()
                    {
                        MatchMembers = new List<MatchMemberModelNew>()
                        {
                            new MatchMemberModelNew(){IsHome = true, Name = item1[1].Value<string>()},
                            new MatchMemberModelNew(){IsHome = true, Name = item1[2].Value<string>()}
                        },
                        StartTime = item1[4].Value<DateTime>(), // DateTime.TryParse(, out var startTime) ? startTime : default(DateTime?),
                        SportName = item1[15].Value<string>(),
                        CompetitionName = item1[16].Value<string>(),
                        RegionName = "World"
                    };

                    if(int.TryParse(item1[6].Value<string>(), out var s1) && int.TryParse(item1[7].Value<string>(), out var s2))
                        match.Statistics = new MatchStatModel()
                        {
                            Score = new ScoreModel()
                            {
                                Score1 = s1,
                                Score2 = s2
                            }
                        };

                    bookmaker.Matches.Add(match);

                    var marketItem1 = item1[5] as JArray;

                    var marketItem2 = marketItem1?[0] as JArray;

                    var marketItem3 = marketItem2?[2] as JArray;

                    if(!(marketItem3?[2] is JArray marketItem4))
                        continue;

                    foreach (var marketItem5 in marketItem4)
                    {
                        if (!(marketItem5 is JArray marketItem6))
                            continue;

                        if (marketItem6[1] != null && marketItem6[1] is JArray winMarketArray)
                        {
                            var market= new MarketModel(){Name = "1X2"};
                            if (winMarketArray[1].Value<int>() != 0)
                            {
                                market.Selections.Add(new SelectionModel()
                                {
                                    Name = "1",
                                    Price = ConvertAmericanOddToDecimal(winMarketArray[1].Value<int>())
                                });
                            }
                            if (winMarketArray[3].Value<int>() != 0)
                            {
                                market.Selections.Add(new SelectionModel()
                                {
                                    Name = "X",
                                    Price = ConvertAmericanOddToDecimal(winMarketArray[3].Value<int>())
                                });
                            }
                            if (winMarketArray[5].Value<int>() != 0)
                            {
                                market.Selections.Add(new SelectionModel()
                                {
                                    Name = "2",
                                    Price = ConvertAmericanOddToDecimal(winMarketArray[5].Value<int>())
                                });
                            }

                            if(market.Selections.Count > 0)
                                match.Markets.Add(market);
                        }

                        if (marketItem6[2] != null && marketItem6[2] is JArray handicapMarketArray)
                        {
                            var market = new MarketModel()
                            {
                                Name = "Handicap",
                                MHandicap = handicapMarketArray[1].Value<double>()
                            };

                            if (handicapMarketArray[2].Value<int>() != 0)
                            {
                                market.Selections.Add(new SelectionModel()
                                {
                                    Name = "{t1} ({h})",
                                    Price = ConvertAmericanOddToDecimal(handicapMarketArray[2].Value<int>()),
                                    HandicapSign = market.MHandicap > 0 ? -1 : +1
                                });
                            }

                            if (handicapMarketArray[4].Value<int>() != 0)
                            {
                                market.Selections.Add(new SelectionModel()
                                {
                                    Name = "1",
                                    Price = ConvertAmericanOddToDecimal(handicapMarketArray[4].Value<int>()),
                                    HandicapSign = market.MHandicap > 0 ? +1 : -1
                                });
                            }

                            if(market.Selections.Count > 0)
                                match.Markets.Add(market);
                        }

                        if (marketItem6[3] != null && marketItem6[3] is JArray totalMarketArray)
                        {
                            var market = new MarketModel()
                            {
                                Name = "Total",
                                MHandicap = totalMarketArray[1].Value<double>()
                            };

                            if (totalMarketArray[2].Value<int>() != 0)
                            {
                                market.Selections.Add(new SelectionModel()
                                {
                                    Name = "Over",
                                    Price = ConvertAmericanOddToDecimal(totalMarketArray[2].Value<int>())
                                });
                            }

                            if (totalMarketArray[4].Value<int>() != 0)
                            {
                                market.Selections.Add(new SelectionModel()
                                {
                                    Name = "Under",
                                    Price = ConvertAmericanOddToDecimal(totalMarketArray[4].Value<int>())
                                });
                            }

                            if(market.Selections.Count > 0)
                                match.Markets.Add(market);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return bookmaker;
        }

        private decimal ConvertAmericanOddToDecimal(int odd)
        {
            return odd >= 0 ? odd / 100m + 1 : 100m / odd * -1 + 1;
        }
    }
}
