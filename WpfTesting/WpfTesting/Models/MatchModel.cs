using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfTesting.Annotations;

namespace WpfTesting.Models
{
    public class MatchModel : INotifyPropertyChanged
    {
        private long _id;
        private string _sportName;
        private string _competitionName;
        private string _regionName;
        private List<MarketModel> _markets;
        public long Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        //public bool? IsNeutralVenue { get; set; }

        //public bool? IsSuspended { get; set; }

        public string SportName
        {
            get { return _sportName; }
            set
            {
                _sportName = value;
                OnPropertyChanged(nameof(SportName));
            }
        }

        public string CompetitionName
        {
            get { return _competitionName; }
            set
            {
                _competitionName = value;
                OnPropertyChanged(nameof(CompetitionName));
            }
        }

        public string RegionName
        {
            get { return _regionName; }
            set
            {
                _regionName = value;
                OnPropertyChanged(nameof(RegionName));
            }
        }

        //public LiveStatus LiveStatus { get; set; }

        //public GameStatus GameStatus { get; set; }

        //public MatchStatModel Statistics { get; set; }

        public DateTime? StartTime { get; set; }

        public List<MarketModel> Markets
        {
            get { return _markets; }
            set
            {
                _markets = value;
                OnPropertyChanged(nameof(Markets));
            }
        }

        public List<MatchMemberModel> MatchMembers { get; set; }


        //[JsonIgnore]
        //public long CompetitionId { get; set; }

        //[JsonIgnore]
        //public long? Team1Id { get; set; }

        //[JsonIgnore]
        //public long? Team2Id { get; set; }

        //[JsonIgnore]
        public long BookmakerId { get; set; }

        //[JsonIgnore]
        //public int GameMaxShift { get; set; }

        public override string ToString()
        {
            switch (MatchMembers)
            {
                case null:
                    return "Matchmembers are null";
                default:
                    return MatchMembers.Count < 1 ? "Match members are less than 1" : $"{MatchMembers[0].Name} vs {MatchMembers[1].Name}";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
