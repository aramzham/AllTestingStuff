using AtkritkaTelegramBot.Infrastructure.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using File = System.IO.File;

namespace AtkritkaTelegramBot.Infrastructure.CommandHandlers
{
    public class BaseCommandHandler : ICommandHandler
    {
        protected static string[] _atkritkas;
        protected static Dictionary<long, HashSet<int>> _sentInChats;
        protected static System.Timers.Timer _timer;

        protected ITelegramBotClient _bot;
        protected Random _dice = new Random();

        public BaseCommandHandler(ITelegramBotClient bot, Dictionary<long, HashSet<int>> sentInChats)
        {
            _bot = bot;
            if(_atkritkas is null)
            {
                _atkritkas = File.ReadAllText("./atkritkas.txt").Split(new[] { "------------------------------" },
                StringSplitOptions.RemoveEmptyEntries);
            }

            if (_sentInChats is null)
                _sentInChats = sentInChats;

            if (_timer is null)
            {
                _timer = new System.Timers.Timer(60000);
                _timer.Elapsed += _timer_Elapsed;
                _timer.Start();
            }
        }

        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _timer.Enabled = false;

            var configText = File.ReadAllText("./config.json");
            var config = JsonConvert.DeserializeObject<Config>(configText);
            foreach (var item in _sentInChats)
            {
                if (config.SentAtkritkasByChatId.ContainsKey(item.Key))
                {
                    foreach (var id in item.Value)
                    {
                        if(!config.SentAtkritkasByChatId[item.Key].Contains(id))
                            config.SentAtkritkasByChatId[item.Key].Add(id);
                    }
                }
                else
                {
                    config.SentAtkritkasByChatId[item.Key] = item.Value;
                }
            }

            var newConfig = JsonConvert.SerializeObject(config);
            File.WriteAllText("./config.json", newConfig);

            _timer.Enabled = true;
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