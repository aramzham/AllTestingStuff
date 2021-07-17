using Newtonsoft.Json;
using RandomAnswerGeneration_For_Chats.Messages;
using RandomAnswerGeneration_For_Chats.Models;
using System;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using File = System.IO.File;

namespace RandomAnswerGeneration_For_Chats
{
    class Program
    {
        private static ITelegramBotClient _botClient;

        static void Main(string[] args)
        {
            ManualResetEvent manualResetEvent = new ManualResetEvent(false);

            var configText = File.ReadAllText("../../../config.json");
            var config = JsonConvert.DeserializeObject<Config>(configText);

            _botClient = new TelegramBotClient(config.TelegaKey);

            var me = _botClient.GetMeAsync().Result;
            Console.WriteLine($"Hello, World! I am user {me.Id} and my name is {me.FirstName}.");

            _botClient.OnMessage += Bot_OnMessage;
            _botClient.StartReceiving();

            manualResetEvent.WaitOne();

            //manualResetEvent.Set(); // to stop
        }

        private static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message is not { Type: MessageType.Text })
                return;

            switch (e.Message.Text)
            {
                case "/a_random_answer":
                    await _botClient.SendTextMessageAsync(
                        chatId: message.Chat,
                        text: Resources.GetRandomMessage()
                    );
                    break;
                case "/a_random_profession":
                    await _botClient.SendTextMessageAsync(
                       chatId: message.Chat,
                       text: Resources.GetRandomProfession()
                   );
                    break;
                default:
                    await _botClient.SendTextMessageAsync(
                        chatId: message.Chat,
                        text: "why are you running?"
                    );
                    break;
            }
        }
    }
}
