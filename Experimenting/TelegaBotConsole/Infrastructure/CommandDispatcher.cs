using TelegaBotConsole.Infrastructure.CommandHandlers;
using Telegram.Bot.Types;

namespace TelegaBotConsole.Infrastructure
{
    public class CommandDispatcher
    {
        public virtual ICommandHandler DispatchCommand(Message message)
        {
            switch (message.Text)
            {
                case "/next_match": return new NextMatchHandler();
                case "/squad": return new SquadHandler();
                case "/is_he_playing": return new IsHePlayingHandler();
                default: return new BaseCommandHandler();
            }
        }
    }
}