using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace AtkritkaTelegramBot.Infrastructure.CommandHandlers
{
    public interface ICommandHandler
    {
        Task HandleCommand(Message message);
    }
}