using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegaBotConsole.Infrastructure.CommandHandlers
{
    public interface ICommandHandler
    {
        Task HandleCommand(Message message);
    }
}