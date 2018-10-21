using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TestsOfAllKinds
{
    public class GoodwinFootballParser
    {
        private const string EventsUrl = "https://www.goodwinbet.am/frontend_api/events/";
        private const string FootballJson = "{\"lang\":\"en\",\"service_id\":1,\"sport_id\":1}";

        private HttpClient _client;

        public void Initialize()
        {
            _client = new HttpClient() { Timeout = TimeSpan.FromSeconds(10) };
            _client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36");
            _client.DefaultRequestHeaders.Add("origin", "https://www.goodwinbet.am");
            _client.DefaultRequestHeaders.Add("referer", "https://www.goodwinbet.am/en/live/");
        }

        public BookmakerModel Parse()
        {
            var bookmaker = new BookmakerModel() { Name = "GoodwinFootball", BookmakerNumber = 10 };
            try
            {
                var response = _client.PostAsync(EventsUrl, new StringContent(FootballJson)).GetAwaiter().GetResult();
                var readAsStringAsync = response.Content.ReadAsStringAsync().Result;
                var events = JsonConvert.DeserializeObject<Rootobject>(readAsStringAsync);
                if (events?.events is null) return null;
                foreach (var goodwinEvent in events.events)
                {
                    SportModel sport = null;
                    var existingSport = bookmaker.Sports.FirstOrDefault(x => x.Name == goodwinEvent.sport_name);
                    if (existingSport is null)
                    {
                        sport = new SportModel() { Name = goodwinEvent.sport_name };
                        bookmaker.Sports.Add(sport);
                    }
                    else sport = existingSport;

                    RegionModel region = null;
                    var existingRegion = sport.Regions.FirstOrDefault(x => x.Name == goodwinEvent.category_name);
                    if (existingRegion is null)
                    {
                        region = new RegionModel() { Name = goodwinEvent.category_name };
                        sport.Regions.Add(region);
                    }
                    else region = existingRegion;

                    LeagueModel league = null;
                    var existingLeague = region.Leagues.FirstOrDefault(x => x.Name == goodwinEvent.tournament_name);
                    if (existingLeague is null)
                    {
                        league = new LeagueModel() { Name = goodwinEvent.tournament_name };
                        region.Leagues.Add(league);
                    }
                    else league = existingLeague;

                    if (goodwinEvent.participants is null || goodwinEvent.participants.Length != 2) continue;
                    var match = new MatchModel();
                    match.MatchMembers.Add(new MatchMemberModel()
                    {
                        Team = new TeamModel() { Name = goodwinEvent.participants[0].participant_name },
                        IsHome = goodwinEvent.participants[0].participant_number == 1
                    });
                    match.MatchMembers.Add(new MatchMemberModel()
                    {
                        Team = new TeamModel() { Name = goodwinEvent.participants[1].participant_name },
                        IsHome = goodwinEvent.participants[0].participant_number == 1
                    });
                    match.StartTime = new DateTime(1970, 1, 1).AddSeconds(goodwinEvent.event_dt);
                    match.Statistics = new MatchStatModel { CurrentPeriodString = goodwinEvent.event_result_name };
                    if (!string.IsNullOrEmpty(goodwinEvent.event_result_total)) SetScores(match, goodwinEvent.event_result_total);
                    league.Matches.Add(match);
                    if (goodwinEvent.head_markets is null || goodwinEvent.head_markets.Length == 0) continue;
                    foreach (var headMarket in goodwinEvent.head_markets)
                    {
                        if (headMarket is null || headMarket.outcomes is null || headMarket.outcomes.Length == 0) continue;
                        var market = new MarketModel() { Name = $"{headMarket.result_type_short_name} {headMarket.market_name}", IsSuspended = headMarket.market_suspend };
                        // win market
                        if (headMarket.market_name == "1 X 2")
                        {
                            foreach (var outcome in headMarket.outcomes)
                            {
                                if (outcome.participant_number == 1) market.Selections.Add(new SelectionModel() { Name = "W1", Price = (decimal)outcome.outcome_coef, IsVisible = outcome.outcome_visible });
                                else if (outcome.participant_number is null) market.Selections.Add(new SelectionModel() { Name = "X", Price = (decimal)outcome.outcome_coef, IsVisible = outcome.outcome_visible });
                                else if (outcome.participant_number == 2) market.Selections.Add(new SelectionModel() { Name = "W2", Price = (decimal)outcome.outcome_coef, IsVisible = outcome.outcome_visible });
                            }
                            if (market.Selections.Count != 0) match.Markets.Add(market);
                        }
                        // handicap
                        else if (headMarket.market_name == "Handicap")
                        {
                            foreach (var outcome in headMarket.outcomes)
                            {
                                if (outcome.participant_number == 1) market.Selections.Add(new SelectionModel() { Name = "{t1} ({h})", Price = (decimal)outcome.outcome_coef, IsVisible = outcome.outcome_visible, HandicapSign = outcome.outcome_param.StartsWith("-") ? -1 : +1 });
                                else if (outcome.participant_number == 2) market.Selections.Add(new SelectionModel() { Name = "{t2} ({h})", Price = (decimal)outcome.outcome_coef, IsVisible = outcome.outcome_visible, HandicapSign = outcome.outcome_param.StartsWith("-") ? -1 : +1 });
                            }
                            var first = headMarket.outcomes.FirstOrDefault(x => double.TryParse(x.outcome_param, out var mh));
                            if (first != null) market.MHandicap = Math.Abs(double.Parse(first.outcome_param));
                            if (market.Selections.Count != 0 && market.MHandicap.HasValue) match.Markets.Add(market);
                        }
                        // total
                        else if (headMarket.market_name== "Over/Under")
                        {
                            foreach (var outcome in headMarket.outcomes)
                            {
                                if (outcome.outcome_short_name.StartsWith("O")) market.Selections.Add(new SelectionModel() { Name = "Over", Price = (decimal)outcome.outcome_coef, IsVisible = outcome.outcome_visible });
                                else if (outcome.outcome_short_name.StartsWith("U")) market.Selections.Add(new SelectionModel() { Name = "Under", Price = (decimal)outcome.outcome_coef, IsVisible = outcome.outcome_visible });
                            }
                            var first = headMarket.outcomes.FirstOrDefault(x => double.TryParse(x.outcome_param, out var mh));
                            if (first != null) market.MHandicap = double.Parse(first.outcome_param);
                            if (market.Selections.Count != 0 && market.MHandicap.HasValue) match.Markets.Add(market);
                        }
                    }
                }

                return bookmaker;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        private void SetScores(MatchModel match, string s)
        {
            // "2:2 (1:2,1:0)"
            // "0:0 (0:0) "
            var matches = Regex.Matches(s, "\\d+\\:\\d+");
            if (matches.Count == 0) return;
            if (matches.Count > 1)
            {
                match.Statistics.Score = GetScoreFromString(matches[0].Value);
                match.Statistics.PeriodScores = new List<ScoreModel>() { GetScoreFromString(matches[1].Value) };
                if (matches.Count > 2) match.Statistics.PeriodScores.Add(GetScoreFromString(matches[2].Value));
            }
        }

        private ScoreModel GetScoreFromString(string s)
        {
            var split = s.Split(':');
            if (split.Length != 2 || !int.TryParse(split[0], out var score1) || !int.TryParse(split[1], out var score2)) return null;
            return new ScoreModel() { Score1 = score1, Score2 = score2 };
        }
    }

    public class Rootobject
    {
        public GoodwinEvent[] events { get; set; }
    }

    public class GoodwinEvent
    {
        public int event_weigh { get; set; }
        public object[] event_tag { get; set; }
        public string event_status_type { get; set; }
        public string event_status_desc_name { get; set; }
        public int event_line_position { get; set; }
        public string sport_name { get; set; }
        public Event_Tv event_tv { get; set; }
        public object score_history { get; set; }
        public Participant[] participants { get; set; }
        public int event_dt { get; set; }
        public string[] event_comment_template_comment { get; set; }
        public int tournament_id { get; set; }
        public Head_Markets[] head_markets { get; set; }
        public int sportform_id { get; set; }
        public int event_result_id { get; set; }
        public int sport_weigh { get; set; }
        public int category_id { get; set; }
        public string event_result_name { get; set; }
        public string event_enet_stat_url { get; set; }
        public int category_weigh { get; set; }
        public int[] sportform_result_types { get; set; }
        public string category_name { get; set; }
        public object event_rts_data { get; set; }
        public int tournament_weigh { get; set; }
        public bool tournament_is_summaries { get; set; }
        public int country_id { get; set; }
        public bool category_is_summaries { get; set; }
        public string tournament_name { get; set; }
        public object event_serving { get; set; }
        public object event_enet_id { get; set; }
        public int market_count { get; set; }
        public string event_result_short_name { get; set; }
        public Scoreboard scoreboard { get; set; }
        public string event_name { get; set; }
        public int event_edition { get; set; }
        public string event_result_total { get; set; }
        public Event_Result_Total_Json event_result_total_json { get; set; }
        public int service_id { get; set; }
        public object event_broadcast_url { get; set; }
        public int sport_id { get; set; }
        public int event_id { get; set; }
        public Event_Timer event_timer { get; set; }
    }

    public class Event_Tv
    {
        public string[] countries { get; set; }
        public bool is_hd { get; set; }
        public bool is_fta { get; set; }
    }

    public class Scoreboard
    {
        public By_Result_Type[] by_result_type { get; set; }
        public Total[] total { get; set; }
    }

    public class By_Result_Type
    {
        public Result_Type_Data[] result_type_data { get; set; }
        public int result_type_id { get; set; }
    }

    public class Result_Type_Data
    {
        public Scope_Data[] scope_data { get; set; }
        public int scope_id { get; set; }
    }

    public class Scope_Data
    {
        public int number { get; set; }
        public int value { get; set; }
    }

    public class Total
    {
        public Scope_Data1[] scope_data { get; set; }
        public int scope_id { get; set; }
    }

    public class Scope_Data1
    {
        public int number { get; set; }
        public int value { get; set; }
    }

    public class Event_Result_Total_Json
    {
        public object[] current_score { get; set; }
        public Total1[] total { get; set; }
        public By_Period[] by_period { get; set; }
        public object[] by_period_point { get; set; }
        public object[] by_participants { get; set; }
        public bool show_corner_cards { get; set; }
        public bool show_field_urine { get; set; }
    }

    public class Total1
    {
        public int value { get; set; }
        public int participant_id { get; set; }
        public int scope_id { get; set; }
        public int number { get; set; }
    }

    public class By_Period
    {
        public int value { get; set; }
        public int participant_id { get; set; }
        public int scope_id { get; set; }
        public int number { get; set; }
        public int result_type_id { get; set; }
        public int weigh { get; set; }
    }

    public class Event_Timer
    {
        public string action { get; set; }
        public int timer { get; set; }
        public int? timer2 { get; set; }
        public string timer_vector { get; set; }
        public int timer_factor { get; set; }
    }

    public class Participant
    {
        public int event_id { get; set; }
        public int event_participant_id { get; set; }
        public int participant_id { get; set; }
        public bool? participant_mark_default { get; set; }
        public string participant_mark_name { get; set; }
        public string participant_mark_short_name { get; set; }
        public string participant_name { get; set; }
        public int participant_number { get; set; }
        public string participant_type { get; set; }
    }

    public class Head_Markets
    {
        public int event_id { get; set; }
        public bool market_has_param { get; set; }
        public int market_id { get; set; }
        public string market_name { get; set; }
        public string market_order { get; set; }
        public bool market_suspend { get; set; }
        public int market_template_id { get; set; }
        public int market_template_view_id { get; set; }
        public object market_template_view_schema { get; set; }
        public int market_template_weigh { get; set; }
        public Outcome[] outcomes { get; set; }
        public int result_type_id { get; set; }
        public string result_type_name { get; set; }
        public string result_type_short_name { get; set; }
        public int result_type_weigh { get; set; }
        public int service_id { get; set; }
        public int sport_id { get; set; }
    }

    public class Outcome
    {
        public float outcome_coef { get; set; }
        public int outcome_id { get; set; }
        public string outcome_name { get; set; }
        public string outcome_param { get; set; }
        public float outcome_perc_stat { get; set; }
        public string outcome_short_name { get; set; }
        public string outcome_tag { get; set; }
        public object outcome_tl_header_name { get; set; }
        public object outcome_tl_left_name { get; set; }
        public int outcome_type_id { get; set; }
        public bool outcome_visible { get; set; }
        public int? participant_number { get; set; }
    }
}



