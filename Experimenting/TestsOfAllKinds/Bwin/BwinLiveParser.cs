using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TestsOfAllKinds;

namespace BetConstruct.OddsMarket.Live.Parsers.BL.Parsers.Bwin
{
    public class BwinLiveParser //: ParserBase, IParser
    {
        private readonly string _urlLive = "https://bcdapi.itsfogo.com/v1/bettingoffer/grid/liveHighlightsEvents?maxEventsPerSport=500&amp;x-bwin-accessId=YjU5ZGYwOTMtOWRjNS00Y2M0LWJmZjktMDNhN2FhNGY3NDkw&amp;_=40725116966909325"; // liveUrl

        //private readonly Dictionary<string, string> _headers = new Dictionary<string, string>()
        //{
        //    {"User-Agent",Configs.UserAgent}
        //};

        //public BwinLiveParser(BookmakerModel bookmaker) : base(bookmaker) { }

        private async Task<string> GetJObject()
        {
            var random = new Random();
            var handler = new HttpClientHandler() { Proxy = new WebProxy("75.146.218.153", 55768) };
            var client = new HttpClient(handler);
            client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36");
            while (true)
            {
                var json = await client.GetStringAsync(_urlLive);
                if (!string.IsNullOrWhiteSpace(json)) return json;
                Thread.Sleep(random.Next(800, 1201));
            }
        }

        public BookmakerModelNew Parse()
        {
            var _bookmaker = new BookmakerModelNew();
            try
            {
                //_stopwatch.Restart();
                //_bookmaker.Matches = new List<MatchModel>();

                var result = JsonConvert.DeserializeObject<BwinResponse>(GetJObject().GetAwaiter().GetResult());

                foreach (var groupedEvent in result.Response.GroupedEvents.Values)
                {
                    if (string.IsNullOrWhiteSpace(groupedEvent.SportName))
                        continue;

                    var sportName = groupedEvent.SportName.Trim();

                    foreach (var @event in groupedEvent.Events)
                    {
                        if (string.IsNullOrWhiteSpace(@event.Player1) || string.IsNullOrWhiteSpace(@event.Player2))
                            continue;

                        var regionName = @event.RegionName;
                        var leagueName = @event.League.LeagueName;

                        var match = new MatchModelNew()
                        {
                            StartTime = @event.StartDate,
                            MatchMembers = new List<MatchMemberModelNew>()
                            {
                                new MatchMemberModelNew() { Name = @event.Player1.Trim(), IsHome = true },
                                new MatchMemberModelNew() { Name = @event.Player2.Trim(), IsHome = false }
                            },
                            SportName = sportName,
                            CompetitionName = leagueName,
                            RegionName = regionName
                        };

                        if (@event.ScoreboardSlim.PeriodName == "Suspended")
                        {
                            match.IsSuspended = true;
                        }

                        //if (@event.ScoreboardSlim != null)
                        //{
                        //    if (@event.ScoreboardSlim.Score != null &&
                        //        int.TryParse(@event.ScoreboardSlim.Score.Split(':')[0], out var sc1) &&
                        //        int.TryParse(@event.ScoreboardSlim.Score.Split(':')[1], out var sc2))
                        //    {
                        //        match.Statistics.Score = new ScoreModel() { Score1 = sc1, Score2 = sc2 };
                        //    }

                        //    if (@event.ScoreboardSlim.YellowCards != null)
                        //    {
                        //        match.Statistics.EventScores.Add(EventType.Yellowcard, new ScoreModel() { Score1 = @event.ScoreboardSlim.YellowCards.Palayer1.player1, Score2 = @event.ScoreboardSlim.YellowCards.Palayer2.player2 });
                        //    }

                        //    if (@event.ScoreboardSlim.RedCards != null)
                        //    {
                        //        match.Statistics.EventScores.Add(EventType.Redcard, new ScoreModel() { Score1 = @event.ScoreboardSlim.RedCards.Palayer1.player1, Score2 = @event.ScoreboardSlim.RedCards.Palayer2.player2 });
                        //    }

                        //    if (@event.ScoreboardSlim.Corners != null)
                        //    {
                        //        match.Statistics.EventScores.Add(EventType.Corner, new ScoreModel() { Score1 = @event.ScoreboardSlim.Corners.Palayer1.player1, Score2 = @event.ScoreboardSlim.Corners.Palayer2.player2 });
                        //    }

                        //    if (@event.ScoreboardSlim.Penalties != null)
                        //    {
                        //        match.Statistics.EventScores.Add(EventType.Penalty, new ScoreModel() { Score1 = @event.ScoreboardSlim.Penalties.Palayer1.player1, Score2 = @event.ScoreboardSlim.Penalties.Palayer2.player2 });
                        //    }

                        //    if (@event.ScoreboardSlim.Sets != null)
                        //    {
                        //        for (var i = 0; i < @event.ScoreboardSlim.Sets[0].Length; i++)
                        //        {
                        //            if (int.TryParse(@event.ScoreboardSlim.Sets[0][i], out var score1) && int.TryParse(@event.ScoreboardSlim.Sets[1][i], out var score2))
                        //                match.Statistics.PeriodScores.Add(new ScoreModel() { Score1 = score1, Score2 = score2 });
                        //        }
                        //    }

                        //    if (@event.ScoreboardSlim.Points != null && @event.ScoreboardSlim.Points.Length == 2)
                        //    {
                        //        match.Statistics.GameScore = $"{@event.ScoreboardSlim.Points[0]}:{@event.ScoreboardSlim.Points[1]}";
                        //    }
                        //}

                        foreach (var marketItem in @event.Markets.Values)
                        {
                            if (!groupedEvent.Groups.ContainsKey(marketItem.GroupId))
                                continue;

                            var groupMarket = groupedEvent.Groups[marketItem.GroupId];
                            var market = new MarketModel()
                            {
                                Name = groupMarket.Marketname,
                                IsSuspended = marketItem.Visibility != 1
                            };
                        }

                        
                        if (match.Markets.Count > 0)
                            _bookmaker.Matches.Add(match);
                    }
                }

                //await Validate();
                //await SaveData();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //_logger.LogException(e, $"Parse [{_bookmaker.Name}, {_bookmaker.Id}]");
            }

            return _bookmaker;
        }
    }
}