using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
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
        public BookmakerManagerViewModel()
        {
            GetBookmakers();
        }
        
        public BookmakerModel SelectedBookmaker
        {
            get { return _selectedBookmaker; }
            set
            {
                _selectedBookmaker = GetBookmaker(value.Id);
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(SelectedBookmaker)));
            }
        }

        public MatchModel SelectedMatch
        {
            get { return _match; }
            set
            {
                _match = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(SelectedMatch)));
            }
        }

        public MarketModel SelectedMarket
        {
            get { return _market; }
            set
            {
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
    }
}
