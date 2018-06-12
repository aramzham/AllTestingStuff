using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCSuccinctly.Models
{
    public class ItineraryItem
    {
        [Required(ErrorMessage ="You must specify when the event will occur")]
        public DateTime? When { get; set; }

        [Required(ErrorMessage = "You must enter a description"), MaxLength(140, ErrorMessage ="The description must be less than 140 characters."), DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required(ErrorMessage = "You must specify how long the event will last"), Range(1, 120, ErrorMessage ="Events should last between one minute and 2 hours"), RegularExpression(@"\d{1,3}", ErrorMessage = "Only numbers are allowed in the duration")]
        public int? Duration { get; set; }

        public bool IsActive { get; set; }

        public bool? Confirmed { get; set; }

        //[UIHint("Phone")]
        public string ContactNumber { get; set; }
    }
}