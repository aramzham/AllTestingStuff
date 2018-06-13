using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCSuccinctly.Models.Attributes
{
    public class ItineraryItemAttributes
    {
        [Required(ErrorMessage =
            "You must specify when this event will occur")]
        [Remote("VerifyAvailability", "Itinerary",
            AdditionalFields = "Description")]
        public object When { get; set; }
        [Required(ErrorMessage = "You must enter a description")]
        [MaxLength(140, ErrorMessage =
            "The description must be less than 140 characters.")]
        public object Description { get; set; }
        [Required(ErrorMessage =
            "You must specify how long the event will last")]
        [Range(1, 120, ErrorMessage =
            "Events should last between one minute and 2 hours")]
        [RegularExpression(@"\d{1,3}",
            ErrorMessage = "Only numbers are allowed in the duration")]
        public object Duration { get; set; }
    }
}