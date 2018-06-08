using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCSuccinctly.Models
{
    public class ItineraryItem
    {
        public DateTime? When { get; set; }
        public string Description { get; set; }
        public int? Duration { get; set; }
    }
}