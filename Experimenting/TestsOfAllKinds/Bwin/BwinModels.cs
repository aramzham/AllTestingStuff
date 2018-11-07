using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BetConstruct.OddsMarket.Live.Parsers.BL.Parsers.Bwin
{
    public class BwinResponse
    {
        [JsonProperty("response")]
        public Response Response { get; set; }
    }

    public class Response
    {
        [JsonProperty("sports")]
        public Dictionary<int, Sports> Sports { get; set; }
        [JsonProperty("groupedEvents")]
        public Dictionary<int, Groupedevents> GroupedEvents { get; set; }
    }

    public class Sports
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("eventCount")]
        public int EventCount { get; set; }
        [JsonProperty("order")]
        public int Order { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Groupedevents
    {
        [JsonProperty("order")]
        public int Order { get; set; }
        [JsonProperty("name")]
        public string SportName { get; set; }
        [JsonProperty("events")]
        public List<Events> Events { get; set; }
        [JsonProperty("groups")]
        public Dictionary<string, Groups> Groups { get; set; }
        [JsonProperty("columns")]
        public Dictionary<string, Columns> Columns { get; set; }

        public Dictionary<string, sixPackGroup> sixPackGroups { get; set; }
        public Dictionary<string, sixPackColumn> sixPackColumns { get; set; }
    }

    public class sixPackGroup
    {
        public string columnId { get; set; }
        public string name { get; set; }
        public string[] optionNames { get; set; }
        public string[] marketGroupIds { get; set; }
        public int order { get; set; }
        public bool balancedMarketsEnabled { get; set; }
        public bool hasAttributeColumn { get; set; }
        public string id { get; set; }
    }

    public class sixPackColumn
    {
        public int order { get; set; }
        public string[] groupIds { get; set; }
        public string id { get; set; }
    }

    public class Events
    {
        [JsonProperty("regionName")]
        public string RegionName { get; set; }
        [JsonProperty("name")]
        public string EventName { get; set; }
        [JsonProperty("player1")]
        public string Player1 { get; set; }
        [JsonProperty("player2")]
        public string Player2 { get; set; }
        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }
        [JsonProperty("league")]
        public League League { get; set; }
        [JsonProperty("isLive")]
        public string IsLive { get; set; }
        [JsonProperty("markets")]
        public Dictionary<int, Markets> Markets { get; set; }
        [JsonProperty("scoreboardSlim")]
        public ScoreboardSlim ScoreboardSlim { get; set; }
    }

    public class League
    {
        [JsonProperty("name")]
        public string LeagueName { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        public int parentLeagueId { get; set; }
    }

    public class Markets
    {
        [JsonProperty("groupId")]
        public string GroupId { get; set; }
        public Dictionary<int, Options> Options { get; set; }
        [JsonProperty("categoryAttribute")]
        public string CategoryAttribute { get; set; }
        [JsonProperty("gameTemplateId")]
        public int GameTemplateId { get; set; }
        [JsonProperty("categoryLevel")]
        public int CategoryLevel { get; set; }
        [JsonProperty("visibility")]
        public int Visibility { get; set; }
    }

    public class ScoreboardSlim
    {
        [JsonProperty("period")]
        public string PeriodName { get; set; }
        [JsonProperty("periodId")]
        public int PeriodId { get; set; }
        [JsonProperty("score")]
        public string Score { get; set; }
        [JsonProperty("started")]
        public string Started { get; set; }
        [JsonProperty("redCards")]
        public RedCards RedCards { get; set; }
        [JsonProperty("substitutions")]
        public Substitutions Substitutions { get; set; }
        [JsonProperty("yellowCards")]
        public YellowCards YellowCards { get; set; }
        [JsonProperty("penalties")]
        public Penalties Penalties { get; set; }
        [JsonProperty("corners")]
        public Corners Corners { get; set; }
        [JsonProperty("points")]
        public string[] Points { get; set; }
        [JsonProperty("sets")]
        public string[][] Sets { get; set; }
    }

    public class RedCards
    {
        [JsonProperty("player1")]
        public Player1 Palayer1 { get; set; }
        [JsonProperty("player2")]
        public Player2 Palayer2 { get; set; }
    }

    public class YellowCards
    {
        [JsonProperty("player1")]
        public Player1 Palayer1 { get; set; }
        [JsonProperty("player2")]
        public Player2 Palayer2 { get; set; }
    }

    public class Penalties
    {
        [JsonProperty("player1")]
        public Player1 Palayer1 { get; set; }
        [JsonProperty("player2")]
        public Player2 Palayer2 { get; set; }
    }
    public class Corners
    {
        [JsonProperty("player1")]
        public Player1 Palayer1 { get; set; }
        [JsonProperty("player2")]
        public Player2 Palayer2 { get; set; }
    }
    public class Substitutions
    {
        [JsonProperty("player1")]
        public Player1 Palayer1 { get; set; }
        [JsonProperty("player2")]
        public Player2 Palayer2 { get; set; }
    }

    public class Player1
    {
        [JsonProperty("255")]
        public int player1 { get; set; }
    }

    public class Player2
    {
        [JsonProperty("255")]
        public int player2 { get; set; }
    }
    
    public class Options
    {
        [JsonProperty("odds")]
        public decimal Odds { get; set; }
        [JsonProperty("order")]
        public int Order { get; set; }
        [JsonProperty("optionAttribute")]
        public string OptionAttribute { get; set; }

        public int visibility { get; set; }
    }

    public class Groups
    {
        [JsonProperty("columnId")]
        public string ColumnId { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Marketname { get; set; }
        [JsonProperty("order")]
        public int Order { get; set; }
        [JsonProperty("optionNames")]
        public string[] OptionNames { get; set; }
        [JsonProperty("balancedMarketsEnabled")]
        public bool BalancedMarketsEnabled { get; set; }
        [JsonProperty("hasAttributeColumn")]
        public bool HasAttributeColumn { get; set; }
    }


    public class Columns
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("order")]
        public int Order { get; set; }
        [JsonProperty("groupIds")]
        public string[] GroupIds { get; set; }
    }
}
