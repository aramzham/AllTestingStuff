using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCSuccinctly.Models
{
    public class ItineraryItem
    {
        public DateTime? When { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public int? Duration { get; set; }
        public bool IsActive { get; set; }
        public bool? Confirmed { get; set; }
    }
}