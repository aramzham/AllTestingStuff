using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Newtonsoft.Json;
using TelegaBotConsole.Infrastructure.Models;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegaBotConsole.Infrastructure.CommandHandlers
{
    public class NextMatchHandler : BaseCommandHandler
    {
        private const string _teamUrl = "https://www.asroma.com";
        private const string _nearestMatchUrl = "https://www.asroma.com/api/s3?q=nearest-matches-en.json";
        private const string _liveMatchUrl = "https://www.asroma.com/api/s3/live?q=live-matches.json&t={timestamp}";

        private static DateTime _nextMatchStartTime = new DateTime(1970, 1, 1);
        private static bool _isLive = true;

        public override async Task HandleCommand(Message message)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36");
                    httpClient.DefaultRequestHeaders.Add("sec-fetch-site", "same-origin");
                    httpClient.DefaultRequestHeaders.Add("sec-fetch-mode", "cors");
                    httpClient.DefaultRequestHeaders.Add("referer", "https://www.asroma.com/en/schedule");
                    httpClient.DefaultRequestHeaders.Add("pragma", "no-cache");

                    NextMatchRootObject nearestMatchObj = null;
                    while (true)
                    {
                        var result = await httpClient.GetStringAsync(_isLive ? _liveMatchUrl : _nearestMatchUrl);
                        nearestMatchObj = JsonConvert.DeserializeObject<NextMatchRootObject>(result);
                        if (!nearestMatchObj.data.Any() && _isLive)
                        {
                            _isLive = false;
                            continue;
                        }
                        break;
                    }

                    var nearestMatch = nearestMatchObj.data.FirstOrDefault(x => x.status == (_isLive ? "in-progress" : "not-started"));
                    var competitionName = nearestMatch.competition.name;
                    var venueText = _isLive ? nearestMatch.venue.name : $"{nearestMatch.venue.name}, {nearestMatch.venue.city}";
                    var homeTeamName = nearestMatch.teams.home.name;
                    var awayTeamName = nearestMatch.teams.away.name;
                    _nextMatchStartTime = nearestMatch.startDate;
                    var otherTeamCrestUrl = homeTeamName == "Roma" ? nearestMatch.teams.away.logo.id : nearestMatch.teams.home.logo.id;
                    var officialLink = _isLive ? nearestMatch.link.url : nearestMatch.customCta[0].link.url;

                    var text = $"{competitionName} | {homeTeamName} vs {awayTeamName} | {_nextMatchStartTime} | {venueText}";

                    Message mes = await _bot.SendPhotoAsync(
                        chatId: message.Chat,
                        photo: $"https://res.cloudinary.com/asroma2-production/image/upload/c_fit,dpr_2.0,f_auto,g_center,h_38,q_auto/v1/{otherTeamCrestUrl}",
                        caption: $"<b>{text}</b>. <i>Source</i>: <a href=\"{(_isLive ? $"{_teamUrl}/en{officialLink}" : officialLink)}\">Official</a>",
                        parseMode: ParseMode.Html
                    );
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                await _bot.SendTextMessageAsync(
                    chatId: message.Chat,
                    text: "No match info"
                );
            }
        }
    }
}