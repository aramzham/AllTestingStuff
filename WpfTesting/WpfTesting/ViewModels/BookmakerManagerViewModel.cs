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
        }

        public ObservableCollection<string> History { get; set; } = new ObservableCollection<string>();

        public BookmakerModel SelectedBookmaker
        {
            get { return _selectedBookmaker; }
            set
            {
                _timer?.Stop();
                lock (_padlock) _selectedBookmaker = GetBookmaker(value.Id);
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
