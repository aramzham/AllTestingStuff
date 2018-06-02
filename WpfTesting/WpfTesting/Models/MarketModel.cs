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
    public class MarketModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public double? MHandicap { get; set; }

        public bool? IsSuspended { get; set; }

        public int? Sequence { get; set; }

        public int? PointSequence { get; set; }

        //public MarketTypeModel MarketType { get; set; }

        public List<SelectionModel> Selections { get; set; }

        //[JsonIgnore]
        public long MatchId { get; set; }

        public override string ToString()
        {
            return !string.IsNullOrEmpty(Name) ? (MHandicap.HasValue == false ? Name : $"{Name} ({MHandicap.Value})") : "Market has no name";
        }

        public override bool Equals(object obj)
        {
            if (!(obj is MarketModel other)) return false;
            if (this.Name != other.Name || this.MHandicap != other.MHandicap) return false;
            if (this.Selections is null && other.Selections != null) return false;
            if (this.Selections != null && other.Selections is null) return false;
            if (this.Selections is null && other.Selections is null) return true;
            if (this.Selections.Count != other.Selections.Count) return false;
            if (this.Selections.Count == 0) return true;
            if (this.Selections[0].HandicapSign != other.Selections[0].HandicapSign) return false;
            return true;
        }

        public static bool operator ==(MarketModel left, MarketModel right)
        {
            if (System.Object.ReferenceEquals(left, null))
            {
                if (System.Object.ReferenceEquals(right, null)) return true;
                return false;
            }
            return left.Equals(right);
        }

        public static bool operator !=(MarketModel left, MarketModel right)
        {
            return !(left == right);
        }
    }
}
