using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using TelegramBotMvcApp.Models.Commands;

namespace TelegramBotMvcApp.Models
{
    public static class Bot
    {
        private static TelegramBotClient _client;
        private static List<Command> _commandsList;
        public static IReadOnlyList<Command> Commands => _commandsList.AsReadOnly();

        public static async Task<TelegramBotClient> Get()
        {
            if (_client != null)
                return _client;

            _commandsList = new List<Command>();
            _commandsList.Add(new HelloCommand());
            // add more commands

            _client = new TelegramBotClient(AppSettings.Key);
            var hook = $"{AppSettings.Url}api/message/update";
            await _client.SetWebhookAsync(hook);

            return _client;
        }
    }
}