using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using Newtonsoft.Json;
using WpfTesting.Infrastructure;
using WpfTesting.Models;

namespace WpfTesting.ViewModels
{
    public class BookmakerManagerViewModel : ObservableCollection<BookmakerModel>
    {
        private OddsMarketLiveClient _liveClient = new OddsMarketLiveClient();
        private BookmakerModel _selectedBookmaker;
        private MatchModel _match;
        private MarketModel _market;
        private System.Timers.Timer _timer = new Timer(500);
        private long _selectedBookmakerId;
        private object _padlock = new object();

        public BookmakerManagerViewModel()
        {
            //GetBookmakers();
            _timer.Elapsed += TimerElapsedEventHandler;
            //_timer.Start();

            Add(new BookmakerModel()
            {
                Id = 1,
                Name = "Aram",
                Matches = new List<MatchModel> {
                    new MatchModel() {
                        MatchMembers = new List<MatchMemberModel>() {
                            new MatchMemberModel() { Name = "Team 1" }, new MatchMemberModel() { Name = "Team 2" } },
                        Markets = new List<MarketModel>() {
                            new MarketModel() { Name = "Result", MHandicap = null, Selections = new List<SelectionModel>() { new SelectionModel() { Name = "W1", Price = 3.1m }, new SelectionModel() { Name = "W2", Price = 1.8m } } },
                        new MarketModel(){ Name="Over/Under", MHandicap=2.5, Selections= new List<SelectionModel>(){ new SelectionModel() { Name = "1", Price = 2.2m }, new SelectionModel() { Name = "2", Price = 1.1m } } } } } }
            });
            Add(new BookmakerModel()
            {
                Id = 2,
                Name = "Vanik",
                Matches = new List<MatchModel> {
                    new MatchModel() {
                        MatchMembers = new List<MatchMemberModel>() {
                            new MatchMemberModel() { Name = "Team 3" }, new MatchMemberModel() { Name = "Team 4" } },
                        Markets = new List<MarketModel>() {
                            new MarketModel() { Name = "Result", MHandicap = null, Selections = new List<SelectionModel>() { new SelectionModel() { Name = "W1", Price = 3.1m }, new SelectionModel() { Name = "W2", Price = 1.8m } } },
                        new MarketModel(){ Name="Over/Under", MHandicap=2.5, Selections= new List<SelectionModel>(){ new SelectionModel() { Name = "1", Price = 2.2m }, new SelectionModel() { Name = "2", Price = 1.1m } } } } }, new MatchModel(){ MatchMembers = new List<MatchMemberModel>(){ new MatchMemberModel() { Name="aranc market 1"}, new MatchMemberModel() { Name= "aranc market 2"} } } }
            });

            History.Add(new HistoryModel() { SelectionName = "a", From = 0, To = 1, Change = ChangeEnum.Up });
            History.Add(new HistoryModel() { SelectionName = "b", From = 0, To = 1, Change = ChangeEnum.Down });
            History.Add(new HistoryModel() { SelectionName = "c", From = 0, To = 1, Change = ChangeEnum.Created });
        }

        public ObservableCollection<HistoryModel> History { get; set; } = new ObservableCollection<HistoryModel>();

        public BookmakerModel SelectedBookmaker
        {
            get { return _selectedBookmaker; }
            set
            {
                _timer?.Stop();
                //lock (_padlock) _selectedBookmaker = GetBookmaker(value.Id);
                _selectedBookmaker = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(SelectedBookmaker)));
            }
        }

        public MatchModel SelectedMatch
        {
            get { return _match; }
            set
            {
                _timer?.Stop();
                _match = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(SelectedMatch)));
            }
        }

        public MarketModel SelectedMarket
        {
            get { return _market; }
            set
            {
                if (_timer.Enabled == false) _timer.Start();
                if (SelectedMarket != value) History.Clear();
                else HistoryHelper.AddHistory(SelectedMarket, value, History);
                _market = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(SelectedMarket)));
            }
        }

        private BookmakerModel GetBookmaker(long id)
        {
            var result = _liveClient.GetMatches(id);
            if (result.StartsWith("\"There is")) return null;
            return JsonConvert.DeserializeObject<BookmakerModel>(result);
        }

        private async void GetBookmakers()
        {
            var result = await _liveClient.GetBookmakers().ConfigureAwait(false);
            var bookmakers = JsonConvert.DeserializeObject<List<BookmakerModel>>(result);
            foreach (var bookmaker in bookmakers)
            {
                System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    Add(bookmaker);
                });
            }
        }

        private void TimerElapsedEventHandler(object sender, ElapsedEventArgs e) // TODO: Add history clearing when market is changed, be careful with market selecting with timer
        {
            if (SelectedMarket is null || SelectedMatch is null) return;

            _timer.Enabled = false;
            BookmakerModel bookmaker = null;
            lock (_padlock) { bookmaker = GetBookmaker(_selectedBookmaker.Id); }

            if (SelectedMatch is null) return;
            //var presentMatch = bookmaker.Matches.FirstOrDefault(x => x.SportName == SelectedMatch.SportName &&
            //                                                         x.CompetitionName == SelectedMatch.CompetitionName &&
            //                                                         x.MatchMembers[0].Name == SelectedMatch.MatchMembers[0].Name &&
            //                                                         x.MatchMembers[1].Name == SelectedMatch.MatchMembers[1].Name);
            var presentMatch = bookmaker.Matches.FirstOrDefault(x => x == SelectedMatch);
            if (presentMatch is null) SelectedMatch = null;
            if (SelectedMarket is null) return;
            //var presentMarket = presentMatch.Markets.FirstOrDefault(x => x.Name == SelectedMarket.Name &&
            //                                                             x.MHandicap == SelectedMarket.MHandicap &&
            //                                                             x.Selections?.Count == SelectedMarket.Selections?.Count &&
            //                                                             x.Selections[0].HandicapSign == SelectedMarket.Selections[0].HandicapSign);
            var presentMarket = presentMatch.Markets.FirstOrDefault(x => x == SelectedMarket);
            if (presentMarket is null) SelectedMatch = presentMatch;
            else
            {
                SelectedMarket = presentMarket;
            }

            _timer.Enabled = true;
        }
    }
}
