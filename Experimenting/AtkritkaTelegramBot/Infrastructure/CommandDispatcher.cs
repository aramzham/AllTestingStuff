using System.Collections.Generic;
using AtkritkaTelegramBot.Infrastructure.CommandHandlers;
using Telegram.Bot;
using Telegram.Bot.Types;
using BaseCommandHandler = AtkritkaTelegramBot.Infrastructure.CommandHandlers.BaseCommandHandler;
using ICommandHandler = AtkritkaTelegramBot.Infrastructure.CommandHandlers.ICommandHandler;

namespace AtkritkaTelegramBot.Infrastructure
{
    public class CommandDispatcher
    {
        private ITelegramBotClient _botClient;
        private Dictionary<long, HashSet<int>> _sentInChats;

        public CommandDispatcher(ITelegramBotClient botClient, Dictionary<long, HashSet<int>> sentInChats)
        {
            _botClient = botClient;
            _sentInChats = sentInChats;
        }

        public virtual ICommandHandler DispatchCommand(Message message)
        {
            switch (message.Text)
            {
                case "/send_atkritka": return new SendAtrkitkaHandler(_botClient, _sentInChats);
                default: return new BaseCommandHandler(_botClient, _sentInChats);
            }
        }
    }
}