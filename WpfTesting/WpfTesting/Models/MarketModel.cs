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
    }
}
