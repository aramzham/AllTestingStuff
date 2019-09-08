using System;
using System.Collections.Generic;
using System.Text;

namespace AtkritkaTelegramBot.Infrastructure.Models
{
    public class Config
    {
        public string TelegaKey { get; set; }
        public Dictionary<long, HashSet<int>> SentAtkritkasByChatId { get; set; }
    }
}
