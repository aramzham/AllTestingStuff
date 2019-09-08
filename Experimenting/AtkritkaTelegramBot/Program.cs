using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using AtkritkaTelegramBot.Infrastructure;
using AtkritkaTelegramBot.Infrastructure.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace AtkritkaTelegramBot
{
    class Program
    {
        private static ITelegramBotClient _botClient;
        private static Dictionary<long, HashSet<int>> _sentInChats;

        static void Main()
        {
            ManualResetEvent manualResetEvent = new ManualResetEvent(false);

            var configText = File.ReadAllText("./config.json");
            var config = JsonConvert.DeserializeObject<Config>(configText);

            _botClient = new TelegramBotClient(config.TelegaKey);
            _sentInChats = config.SentAtkritkasByChatId;

            var me = _botClient.GetMeAsync().Result;
            Console.WriteLine($"Hello, World! I am user {me.Id} and my name is {me.FirstName}.");

            _botClient.OnMessage += Bot_OnMessage;
            _botClient.StartReceiving();

            manualResetEvent.WaitOne();

            //manualResetEvent.Set(); // to stop
        }

        static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text != null)
            {
                Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}, text = {e.Message.Text}.");

                if (e.Message.Text.StartsWith("/"))
                {
                    var dispatcher = new CommandDispatcher(_botClient, _sentInChats);
                    var handler = dispatcher.DispatchCommand(e.Message);
                    await handler.HandleCommand(e.Message);
                }
                else
                {
                    //await _botClient.SendTextMessageAsync(
                    //    chatId: e.Message.Chat,
                    //    text: "You said:\n" + e.Message.Text
                    //);

                    //await _botClient.SendStickerAsync(
                    //    chatId: e.Message.Chat,
                    //    sticker: "https://github.com/TelegramBots/book/raw/master/src/docs/sticker-dali.webp"
                    //);

                    //await _botClient.SendVideoAsync(
                    //    chatId: e.Message.Chat,
                    //    video: "https://github.com/TelegramBots/book/raw/master/src/docs/video-bulb.mp4"
                    //);

                    //Message message = await _botClient.SendPhotoAsync(
                    //    chatId: e.Message.Chat,
                    //    photo: "https://github.com/TelegramBots/book/raw/master/src/docs/photo-ara.jpg",
                    //    caption: "<b>Ara bird</b>. <i>Source</i>: <a href=\"https://pixabay.com\">Pixabay</a>",
                    //    parseMode: ParseMode.Html
                    //);

                    //Message msg1 = await _botClient.SendStickerAsync(
                    //    chatId: e.Message.Chat,
                    //    sticker: "https://github.com/TelegramBots/book/raw/master/src/docs/sticker-fred.webp"
                    //);

                    //Message msg2 = await _botClient.SendStickerAsync(
                    //    chatId: e.Message.Chat,
                    //    sticker: msg1.Sticker.FileId
                    //);

                    //Message msg = await _botClient.SendAudioAsync(
                    //    e.Message.Chat,
                    //    "https://github.com/TelegramBots/book/raw/master/src/docs/audio-guitar.mp3"
                    ///* ,
                    //performer: "Joel Thomas Hunger",
                    //title: "Fun Guitar and Ukulele",
                    //duration: 91 // in seconds
                    //*/
                    //);

                    //Message msgVideo = await _botClient.SendVideoAsync(
                    //    chatId: e.Message.Chat,
                    //    video: "https://raw.githubusercontent.com/TelegramBots/book/master/src/docs/video-countdown.mp4",
                    //    thumb: "https://raw.githubusercontent.com/TelegramBots/book/master/src/2/docs/thumb-clock.jpg",
                    //    supportsStreaming: true
                    //);

                    //// polls can't be sent to private chats
                    //Message pollMessage = await _botClient.SendPollAsync(
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
