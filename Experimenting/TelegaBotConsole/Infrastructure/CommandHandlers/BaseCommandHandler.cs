using System.Threading.Tasks;
using TelegaBotConsole.Infrastructure.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegaBotConsole.Infrastructure.CommandHandlers
{
    public class BaseCommandHandler : ICommandHandler
    {
        protected static MatchInfoModel MatchInfo = new MatchInfoModel();
        protected ITelegramBotClient _bot;

        public BaseCommandHandler()
        {
            _bot = new TelegramBotClient("824066216:AAEUhgxegF5CgRdq5qs5E_aDOkiz6gfmSK8");
        }

        public virtual async Task HandleCommand(Message message)
        {
            await _bot.SendTextMessageAsync(
                chatId: message.Chat,
                text: "You said:\n" + message.Text
            );
        }
    }
}