using System;

namespace TelegaBotConsole.Infrastructure.Models
{
    public class MatchInfoModel
    {
        public string OpponentName { get; set; }
        public DateTime StartTime { get; set; } = new DateTime(1970, 1, 1);
        public bool IsHome { get; set; }
        public bool IsLive { get; set; } = true;
        public SquadModel Squad { get; set; }
    }
}