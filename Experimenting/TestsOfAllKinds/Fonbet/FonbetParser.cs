using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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

        private bool _isFirstTime = true;
        private HttpClient _client;

        public void Initialize()
        {
            _client = new HttpClient() { Timeout = TimeSpan.FromSeconds(10) };
            _client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36");
        }

        public BookmakerModelNew Parse()
        {
            var bookmaker = new BookmakerModelNew();
            try
            {
                var marketDict = new Dictionary<int, Tuple<string, string>>();
                if (_isFirstTime)
                {
                    var catalogJson = _client.GetStringAsync(CatalogUrl).GetAwaiter().GetResult();
                    var catalog = JsonConvert.DeserializeObject<RootCatalogObject>(catalogJson);
                    foreach (var mainCatalogGrids in catalog.catalog[0].grids)
                    {
                        if (mainCatalogGrids.grid.Length < 2)
                            continue;
                        for (int i = 0; i < mainCatalogGrids.grid[1].Length; i++)
                        {
                            if (mainCatalogGrids.grid[1][i].kind != "value")
                                continue;
                            marketDict.Add(mainCatalogGrids.grid[1][i].factorId, Tuple.Create(mainCatalogGrids.grid[0][i].eng, 
                                !string.IsNullOrEmpty(mainCatalogGrids.name_eng) 
                                    ? mainCatalogGrids.name_eng
                                    : mainCatalogGrids.grid[1][0].eng));
                        }
                    }

                    _isFirstTime = false;
                }

                var modelsJson = _client.GetStringAsync(MainUrl).GetAwaiter().GetResult();
                var root = JsonConvert.DeserializeObject<RootModelObject>(modelsJson);
                var sports = root.sports.Where(x => x.kind == "sport").ToDictionary(k => k.id, v => v.name);
                var leagues = root.sports.Where(x => x.kind == "segment").ToDictionary(k => k.id, v => v);
                var blockedEvents = new HashSet<int>(root.eventBlocks.Select(x => x.eventId));
                var stats = root.eventMiscs.ToDictionary(k => k.id, v => new MatchStatModel()
                {
                    Info = v.comment,
                    Score = new ScoreModel() { Score1 = v.score1, Score2 = v.score2 }
                });
                var matchesLevel1 = root.events.Where(x => x.level == 1).ToDictionary(k => k.id, v => new MatchModelNew()
                {
                    StartTime = new DateTime(1970, 1, 1).AddSeconds(v.startTime),
                    MatchMembers = new List<MatchMemberModelNew>()
                    {
                        new MatchMemberModelNew(){IsHome = true, Name = v.team1},
                        new MatchMemberModelNew(){IsHome = false, Name = v.team2}
                    },
                    SportName = !leagues.ContainsKey(v.sportId) || !sports.ContainsKey(leagues[v.sportId].parentId) ? "no sport name" : sports[leagues[v.sportId].parentId],
                    CompetitionName = leagues.ContainsKey(v.sportId) ? leagues[v.sportId].name : "no league name",
                    IsSuspended = blockedEvents.Contains(v.id),
                    Statistics = stats.ContainsKey(v.id) ? stats[v.id] : null
                });
                var matchesOthers = root.events.Where(x => x.level != 1).ToDictionary(k => k.id, v => v);

                MatchModelNew match = null;
                foreach (var customFactor in root.customFactors)
                {
                    if (customFactor is null || !marketDict.ContainsKey(customFactor.f))
                        continue;

                    var marketPrefix = string.Empty;
                    if (matchesLevel1.ContainsKey(customFactor.e))
                    {
                        match = matchesLevel1[customFactor.e];
                    }
                    else if (matchesOthers.ContainsKey(customFactor.e) && matchesLevel1.ContainsKey(matchesOthers[customFactor.e].parentId))
                    {
                        match = matchesLevel1[matchesOthers[customFactor.e].parentId];
                        marketPrefix = $"{matchesOthers[customFactor.e].name} ";
                    }
                    else
                        continue;

                    var selection = new SelectionModel()
                    {
                        Name = marketDict[customFactor.f].Item1,
                        Price = (decimal)customFactor.v
                    };
                    if (!string.IsNullOrEmpty(customFactor.pt) && customFactor.pt.StartsWith("-"))
                        selection.HandicapSign = -1;
                    else if (!string.IsNullOrEmpty(customFactor.pt) && customFactor.pt.StartsWith("+"))
                        selection.HandicapSign = +1;

                    var existingMarket = match.Markets.FirstOrDefault(x => x.Name == $"{marketPrefix}{marketDict[customFactor.f].Item2}");
                    if (existingMarket is null)
                    {
                        match.Markets.Add(new MarketModel()
                        {
                            Name = $"{marketPrefix}{marketDict[customFactor.f].Item2}",
                            MHandicap = !string.IsNullOrEmpty(customFactor.pt) && double.TryParse(customFactor.pt, out var hdp) ? Math.Abs(hdp) : default(double?),
                            Selections = new List<SelectionModel>() { selection }
                        });
                    }
                    else
                    {
                        existingMarket.Selections.Add(selection);
                    }
                }

                bookmaker.Matches = matchesLevel1.Values.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return bookmaker;
        }
    }
}
