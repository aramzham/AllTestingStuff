using Newtonsoft.Json;
using RandomAnswerGeneration_For_Chats.Messages;
using RandomAnswerGeneration_For_Chats.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using File = System.IO.File;

var manualResetEvent = new ManualResetEvent(false);

var configText = File.ReadAllText("../../../config.json");
var config = JsonConvert.DeserializeObject<Config>(configText);

var bot = new TelegramBotClient(config.TelegaKey);

using CancellationTokenSource cts = new ();

// StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
var receiverOptions = new ReceiverOptions()
{
    AllowedUpdates = [] // receive all update types except ChatMember related updates
};

bot.StartReceiving(
    updateHandler: HandleUpdateAsync,
    pollingErrorHandler: HandlePollingErrorAsync,
    receiverOptions: receiverOptions,
    cancellationToken: cts.Token
);

var me = await bot.GetMeAsync();
Console.WriteLine($"Start listening for @{me.Username}");

manualResetEvent.WaitOne();

//manualResetEvent.Set(); // to stop

// Send cancellation request to stop bot
cts.Cancel();

async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    // Only process Message updates: https://core.telegram.org/bots/api#message
    if (update.Message is not { } message)
        return;
    // Only process text messages
    if (message.Text is not { } messageText)
        return;

    var chatId = message.Chat.Id;

    Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

    switch (message.Text)
    {
        case "/a_random_answer":
            await botClient.SendTextMessageAsync(
                chatId: message.Chat,
                text: Resources.GetRandomMessage()
            );
            break;
        case "/a_random_profession":
            await botClient.SendTextMessageAsync(
                chatId: message.Chat,
                text: Resources.GetRandomProfession()
            );
            break;
        default:
            await botClient.SendTextMessageAsync(
                chatId: message.Chat,
                text: "why are you running?"
            );
            break;
    }
}

Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
    var errorMessage = exception switch
    {
        ApiRequestException apiRequestException
            => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
        _ => exception.ToString()
    };

    Console.WriteLine(errorMessage);
    return Task.CompletedTask;
}
