using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegaBotConsole.Infrastructure.CommandHandlers
{
    public class IsHePlayingHandler : SquadHandler
    {
        public override async Task HandleCommand(Message message)
        {
            var text = "Ինչ Մխիթարյան, որ squad info չկա?";

            if (MatchInfo.Squad?.StartingEleven is null)
            {
                var players = await base.GetSquad();
                if (players != null)
                    MatchInfo.Squad = players;
            }

            if (MatchInfo.Squad?.StartingEleven != null)
            {
                if (MatchInfo.Squad.StartingEleven.Any(x => x.Contains("Mkhitaryan")))
                {
                    await _bot.SendStickerAsync(
                        chatId: message.Chat,
                        sticker: "https://github.com/TelegramBots/book/raw/master/src/docs/sticker-dali.webp"
                    );
                    text = "Կարային չխաղացնեին?! :D";
                }
                else if (MatchInfo.Squad.Subs != null && MatchInfo.Squad.Subs.Any(x => x.Contains("Mkhitaryan")))
                {
                    Message msg1 = await _bot.SendStickerAsync(
                        chatId: message.Chat,
                        sticker: "https://github.com/TelegramBots/book/raw/master/src/docs/sticker-fred.webp"
                    );
                    text = "Զամեն ա եղոն :/";
                }
                else
                {
                    Message msg1 = await _bot.SendStickerAsync(
                        chatId: message.Chat,
                        sticker: "https://github.com/TelegramBots/book/raw/master/src/docs/sticker-fred.webp"
                    );
                    text = "Չեն խաղացնում մեր տղուն :(";
                }
            }
            else
            {
                Message msg = await _bot.SendAudioAsync(
                    message.Chat,
                    "https://github.com/TelegramBots/book/raw/master/src/docs/audio-guitar.mp3"
                /* ,
                performer: "Joel Thomas Hunger",
                title: "Fun Guitar and Ukulele",
                duration: 91 // in seconds
                */
                );
            }

            await _bot.SendTextMessageAsync(
                chatId: message.Chat,
                text: text
            );
        }
    }
}