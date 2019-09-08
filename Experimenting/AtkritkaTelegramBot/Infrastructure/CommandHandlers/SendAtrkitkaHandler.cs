using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.IO;

namespace AtkritkaTelegramBot.Infrastructure.CommandHandlers
{
    public class SendAtrkitkaHandler : BaseCommandHandler
    {
        public SendAtrkitkaHandler(ITelegramBotClient botClient, Dictionary<long, HashSet<int>> sentInChats) : base(botClient, sentInChats)
        {
        }

        public override async Task HandleCommand(Message message)
        {
            var index = default(int);
            while (true)
            {
                index = _dice.Next(0, _atkritkas.Length);
                if (_sentInChats.ContainsKey(message.Chat.Id) && !_sentInChats[message.Chat.Id].Contains(index))
                {
                    _sentInChats[message.Chat.Id].Add(index);
                    break;
                }
                else if(!_sentInChats.ContainsKey(message.Chat.Id))
                {
                    _sentInChats[message.Chat.Id] = new HashSet<int>() { index };
                    break;
                }
            }

            await _bot.SendTextMessageAsync(
                chatId: message.Chat,
                text: _atkritkas[index]
            );
        }
    }
}