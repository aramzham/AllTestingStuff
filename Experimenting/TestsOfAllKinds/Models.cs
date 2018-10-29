using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsOfAllKinds
{
    public class SelectionModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int? HandicapSign { get; set; }
        public long? SelectionTypeId { get; set; }
        public string KindOct { get; set; }
        public bool? IsVisible { get; set; }
    }

    public class MarketModel
    {
        public string Name { get; set; }
        public double? MHandicap { get; set; }
        public List<SelectionModel> Selections { get; set; } = new List<SelectionModel>();
        public long? MarketTypeId { get; set; }
        public string KindOct { get; set; }
        public bool? IsSuspended { get; set; }
    }

    public class TeamModel
    {
        public string Name { get; set; }
    }

    public class MatchMemberModel
    {
        public bool IsHome { get; set; }
        public TeamModel Team { get; set; }
    }

    public class ScoreModel
    {
        public int Score1 { get; set; }
        public int Score2 { get; set; }
    }

    public class MatchStatModel
    {
        public Dictionary<string, ScoreModel> EventScores { get; set; }
        public string CurrentPeriodString { get; set; }
        public ScoreModel Score { get; set; }
        public string Info { get; set; }
        public List<ScoreModel> PeriodScores { get; set; }
    }

    public class MatchModel
    {
        public DateTime? StartTime { get; set; }
        public List<MatchMemberModel> MatchMembers { get; set; } = new List<MatchMemberModel>();
        public List<MarketModel> Markets { get; set; } = new List<MarketModel>();
        public bool IsNeutralVenue { get; set; }
        public MatchStatModel Statistics { get; set; }
        public string CurrentTime { get; set; }
        public bool? IsSuspended { get; set; }
    }

    public class LeagueModel
    {
        public string Name { get; set; }
        public List<MatchModel> Matches { get; set; } = new List<MatchModel>();
    }

    public class RegionModel
    {
        public string Name { get; set; }
        public List<LeagueModel> Leagues { get; set; } = new List<LeagueModel>();
    }

    public class SportModel
    {
        public string Name { get; set; }
        public List<RegionModel> Regions { get; set; } = new List<RegionModel>();
        public long? MapId { get; set; }
    }

    public class BookmakerModel
    {
        public long BookmakerNumber { get; set; }
        public string Name { get; set; }
        public List<SportModel> Sports { get; set; } = new List<SportModel>();
        public int ParseDuration { get; set; }
        public DateTime CreationDate { get; set; }
    }

    public class BookmakerModelNew
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<MatchModel> Matches { get; set; } = new List<MatchModel>();
        public int ParseDuration { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
