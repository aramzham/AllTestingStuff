using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TestsOfAllKinds.Fonbet
{
    public class FonbetParser
    {
        private const string CatalogUrl = "https://clientsapi-002.ccf4ab51771cacd46d.com/lineCatalog/Kab";
        private const string MainUrl = "https://line-02.ccf4ab51771cacd46d.com/live/currentLine/en/?0.40725116966909325";

        private Dictionary<int, string> _sports = new Dictionary<int, string>();
        private HashSet<int> _blockedEvents = new HashSet<int>();
        private Dictionary<int, MatchModelNew> _matches = new Dictionary<int, MatchModelNew>();
        private Dictionary<int, MatchStatModel> _stats = new Dictionary<int, MatchStatModel>();
        private HttpClient _client;

        public void Initialize()
        {
            _client = new HttpClient() { Timeout = TimeSpan.FromSeconds(10) };
            _client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36");
        }

        public BookmakerModelNew Parse()
        {
            var bookmaker = new BookmakerModelNew();
            var modelsJson = _client.GetStringAsync(MainUrl).GetAwaiter().GetResult();
            var root = JsonConvert.DeserializeObject<RootModelObject>(modelsJson);
            _sports = root.sports.Where(x => x.kind == "sport").ToDictionary(k => k.id, v => v.name);
            _blockedEvents = new HashSet<int>(root.eventBlocks.Select(x => x.eventId));
            _stats = root.eventMiscs.ToDictionary(k => k.id, v => new MatchStatModel()
            {
                Info = v.comment,
                Score = new ScoreModel() { Score1 = v.score1, Score2 = v.score2 }
            });
            _matches = root.announcements.ToDictionary(k => k.id, v => new MatchModelNew()
            {
                StartTime = new DateTime(1970, 1, 1).AddSeconds(v.startTime),
                MatchMembers = new List<MatchMemberModelNew>()
                {
                    new MatchMemberModelNew(){IsHome = true, Name = v.team1},
                    new MatchMemberModelNew(){IsHome = false, Name = v.team2}
                },
                SportName = _sports.ContainsKey(v.sportId) ? _sports[v.sportId] : "no sport name",
                CompetitionName = v.segmentName,
                IsSuspended = _blockedEvents.Contains(v.id),
                Statistics = _stats.ContainsKey(v.id) ? _stats[v.id] : null
            });


            return bookmaker;
        }
    }
}
