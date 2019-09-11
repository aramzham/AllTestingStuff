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

        public override async Task HandleCommand(Message message)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36");
                    httpClient.DefaultRequestHeaders.Add("sec-fetch-site","same-origin");
                    httpClient.DefaultRequestHeaders.Add("sec-fetch-mode", "cors");
                    httpClient.DefaultRequestHeaders.Add("referer", "https://www.asroma.com/en/schedule");
                    httpClient.DefaultRequestHeaders.Add("pragma", "no-cache");

                    var result = await httpClient.GetStringAsync(_nearestMatchUrl);
                    var nearestMatchObj = JsonConvert.DeserializeObject<NextMatchRootObject>(result);
                    var nearestMatch = nearestMatchObj.data.FirstOrDefault(x => x.status== "not-started");

                    var competitionName = nearestMatch.competition.name;
                    var venueText = $"{nearestMatch.venue.name}, {nearestMatch.venue.city}";
                    var homeTeamName = nearestMatch.teams.home.name;
                    var awayTeamName = nearestMatch.teams.away.name;
                    var date = nearestMatch.startDate;
                    var otherTeamCrestUrl = homeTeamName == "Roma" ? nearestMatch.teams.away.logo.id : nearestMatch.teams.home.logo.id;
                    var officialLink = nearestMatch.customCta[0].link.url;

                    var isLive = false;
                    // there is a live match
                    if (isLive)
                    {
                        //await _bot.SendTextMessageAsync(
                        //    chatId: message.Chat,
                        //    text: "Match is live"
                        //);

                        //var resultText = $"{figcaptions[0].InnerText} {scoreText} {figcaptions[1].InnerText}";

                        //await _bot.SendTextMessageAsync(
                        //    chatId: message.Chat,
                        //    text: resultText
                        //);
                    }
                    // if no live match => get next match
                    else
                    {
                        var text = $"{competitionName} | {homeTeamName} vs {awayTeamName} | {date} | {venueText}";

                        Message mes = await _bot.SendPhotoAsync(
                            chatId: message.Chat,
                            photo: $"https://res.cloudinary.com/asroma2-production/image/upload/c_fit,dpr_2.0,f_auto,g_center,h_38,q_auto/v1/{otherTeamCrestUrl}",
                            caption: $"<b>{text}</b>. <i>Source</i>: <a href=\"{officialLink}\">Official</a>",
                            parseMode: ParseMode.Html
                        );
                    }
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