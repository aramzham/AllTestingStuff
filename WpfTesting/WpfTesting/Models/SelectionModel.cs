using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTesting.Models
{
    public class SelectionModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public decimal? OriginalPrice { get; set; }

        public bool IsSuspended { get; set; }

        public long? HandicapSign { get; set; }

        public decimal? Handicap { get; set; }

        public decimal Price { get; set; }

        //[JsonIgnore]
        public long MarketId { get; set; }
    }
}
