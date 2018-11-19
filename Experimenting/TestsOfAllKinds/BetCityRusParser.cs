using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace TestsOfAllKinds
{
    public class BetCityRusParser
    {
        private string GetData()
        {
            var random = new Random();
            try
            {
                var client = new HttpClient() { Timeout = TimeSpan.FromSeconds(10) };
                while (true)
                {
                    client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.77 Safari/537.36");
                    var response = client.GetStringAsync("https://betcityrus.com/api/v1/live/bets?rev=7&amp;ver=447&amp;csn=f9xg7b&amp;lng=1").GetAwaiter().GetResult();
                    if (!string.IsNullOrEmpty(response))
                        return response;

                    Thread.Sleep(random.Next(1000, 2001));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }

        public BookmakerModelNew Parse()
        {
            var bookmaker = new BookmakerModelNew();

            var data = GetData();
            var json = JObject.Parse(data);
            var reply = json["reply"];
            var sports = reply["sports"];
            foreach (var sport in sports.SelectTokens("*"))
            {
                var sportName = sport["name_sp"].Value<string>();
                var leagues = sport["chmps"];
                foreach (var league in leagues.SelectTokens("*"))
                {
                    var leagueName = league["name_ch"].Value<string>();
                    var events = league["evts"];
                    foreach (var @event in events.SelectTokens("*"))
                    {
                        var match = new MatchModelNew()
                        {
                            StartTime = @event["date_ev_str"].Value<DateTime>(),
                            SportName = sportName,
                            CompetitionName = leagueName,
                            MatchMembers = new List<MatchMemberModelNew>()
                            {
                                new MatchMemberModelNew(){Name = @event["name_ht"].Value<string>(), IsHome = true},
                                new MatchMemberModelNew(){Name = @event["name_at"].Value<string>()}
                            }
                        };

                        var mainMarkets = @event["main"];
                        foreach (var mainMarket in mainMarkets.SelectTokens("*"))
                        {
                            var market = new MarketModel(){ Name = mainMarket["name"].Value<string>()};
                        }
                    }
                }
            }

            return bookmaker;
        }
    }
}