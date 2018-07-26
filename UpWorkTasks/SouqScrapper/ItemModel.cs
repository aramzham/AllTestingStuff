using System;

namespace SouqScrapper
{
    public class ItemModel
    {
    //1- Product page link: https://uae.souq.com/ae-en/anker-soundcore-bluetooth-speaker-black-a3102011-10725120/i/

    //2- Product title: Anker SoundCore Bluetooth Speaker - Black, A3102011
    //3- Reviews Count "just the number": 149
    //4- Rating: 4.6
    //5- Total Rating count: 345
    //6- Final Price: 125
    //7- category name: Speakers
    //8- Item EAN: 2724328370987
    //9- product listing date: 2016/05/12 YYYY/MM/DD
        public string Link { get; set; }
        public string Title { get; set; }
        public int ReviewsCount { get; set; }
        public decimal Rating { get; set; }
        public int TotalRatingCount { get; set; }
        public decimal FinalPrice { get; set; }
        public string CategoryName { get; set; }
        public string EAN { get; set; }
        public DateTime ListingDate { get; set; }
    }
}