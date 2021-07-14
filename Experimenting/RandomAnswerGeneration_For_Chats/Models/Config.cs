using System.Collections.Generic;

namespace RandomAnswerGeneration_For_Chats.Models
{
    public class Config
    {
        public string TelegaKey { get; set; }
        public Dictionary<long, HashSet<int>> SentAtkritkasByChatId { get; set; }
    }
}
