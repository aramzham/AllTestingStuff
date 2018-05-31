using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (string.IsNullOrEmpty(Name)) return "Market has no name";
            else if (MHandicap.HasValue == false) return Name;
            else return $"{Name} ({MHandicap.Value})";
        }
    }
}
