using System.Collections.Generic;
using Telegram.Bot;

namespace AtkritkaTelegramBot.Infrastructure.CommandHandlers
{
    public class JokeOfTheDayHandler : BaseCommandHandler
    {
        public JokeOfTheDayHandler(ITelegramBotClient bot, Dictionary<long, HashSet<int>> sentInChats) : base(bot, sentInChats)
        {
        }
    }
}