using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TelegaBotConsole.Infrastructure;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegaBotConsole
{
    class Program
    {
        static ITelegramBotClient botClient;

        static void Main()
        {
            botClient = new TelegramBotClient(ConfigurationManager.AppSettings["telegaKey"]);

            var me = botClient.GetMeAsync().Result;
            Console.WriteLine($"Hello, World! I am user {me.Id} and my name is {me.FirstName}.");

            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();
            Thread.Sleep(int.MaxValue);
        }

        static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text != null)
            {
                Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}, text = {e.Message.Text}.");

                if (e.Message.Text.StartsWith("/"))
                {
                    var dispatcher = new CommandDispatcher();
                    var handler = dispatcher.DispatchCommand(e.Message);
                    await handler.HandleCommand(e.Message);
                }
                else
                {
                    //await botClient.SendTextMessageAsync(
                    //    chatId: e.Message.Chat,
                    //    text: "You said:\n" + e.Message.Text
                    //);

                    //await botClient.SendStickerAsync(
                    //    chatId: e.Message.Chat,
                    //    sticker: "https://github.com/TelegramBots/book/raw/master/src/docs/sticker-dali.webp"
                    //);

                    //await botClient.SendVideoAsync(
                    //    chatId: e.Message.Chat,
                    //    video: "https://github.com/TelegramBots/book/raw/master/src/docs/video-bulb.mp4"
                    //);

                    //Message message = await botClient.SendPhotoAsync(
                    //    chatId: e.Message.Chat,
                    //    photo: "https://github.com/TelegramBots/book/raw/master/src/docs/photo-ara.jpg",
                    //    caption: "<b>Ara bird</b>. <i>Source</i>: <a href=\"https://pixabay.com\">Pixabay</a>",
                    //    parseMode: ParseMode.Html
                    //);

                    //Message msg1 = await botClient.SendStickerAsync(
                    //    chatId: e.Message.Chat,
                    //    sticker: "https://github.com/TelegramBots/book/raw/master/src/docs/sticker-fred.webp"
                    //);

                    //Message msg2 = await botClient.SendStickerAsync(
                    //    chatId: e.Message.Chat,
                    //    sticker: msg1.Sticker.FileId
                    //);

                    //Message msg = await botClient.SendAudioAsync(
                    //    e.Message.Chat,
                    //    "https://github.com/TelegramBots/book/raw/master/src/docs/audio-guitar.mp3"
                    ///* ,
                    //performer: "Joel Thomas Hunger",
                    //title: "Fun Guitar and Ukulele",
                    //duration: 91 // in seconds
                    //*/
                    //);

                    //Message msgVideo = await botClient.SendVideoAsync(
                    //    chatId: e.Message.Chat,
                    //    video: "https://raw.githubusercontent.com/TelegramBots/book/master/src/docs/video-countdown.mp4",
                    //    thumb: "https://raw.githubusercontent.com/TelegramBots/book/master/src/2/docs/thumb-clock.jpg",
                    //    supportsStreaming: true
                    //);

                    //// polls can't be sent to private chats
                    //Message pollMessage = await botClient.SendPollAsync(
                    //    chatId: e.Message.Chat,//"@group_or_channel_username",
                    //    question: "Did you ever hear the tragedy of Darth Plagueis The Wise?",
                    //    options: new[]
                    //    {
                    //        "Yes for the hundredth time!",
                    //        "No, who`s that?"
                    //    }
                    //);
                }
            }
        }
    }
}
