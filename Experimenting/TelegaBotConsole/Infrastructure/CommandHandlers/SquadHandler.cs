using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using TelegaBotConsole.Infrastructure.Models;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegaBotConsole.Infrastructure.CommandHandlers
{
    public class SquadHandler : BaseCommandHandler
    {
        private const string _flashScoreUrl = "https://d.flashscore.com/x/feed/f_1_0_4_en_1";
        private const string LineupLink = "https://d.flashscore.com/x/feed/d_li_{urlKey}_en_1";
        private const string TEAM_NAME = "AS Roma";
        private string _stickerUrl = "https://github.com/TelegramBots/book/raw/master/src/docs/sticker-fred.webp";

        public override async Task HandleCommand(Message message)
        {
            try
            {
                var squad = await GetSquad();

                if (squad != null)
                {
                    MatchInfo.Squad = squad;

                    await _bot.SendTextMessageAsync(
                        chatId: message.Chat,
                        text: squad.ToHtmlString(),
                        parseMode: ParseMode.Html
                    );
                }
                else
                {
                    await _bot.SendTextMessageAsync(
                        chatId: message.Chat,
                        text: "No squad info for the moment"
                    );

                    await _bot.SendVideoAsync(
                        chatId: message.Chat,
                        video: "https://github.com/TelegramBots/book/raw/master/src/docs/video-bulb.mp4"
                    );
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                await SendSticker(message);
            }
        }

        protected async Task<SquadModel> GetSquad()
        {
            if (DateTime.UtcNow - MatchInfo.StartTime > TimeSpan.FromHours(1))
            {
                MatchInfo.Squad = null;
                return null;
            }

            if (MatchInfo.Squad?.StartingEleven != null)
                return MatchInfo.Squad;

            try
            {
                var urlKey = GetUrlKey(TEAM_NAME, out var isHome);
                if (urlKey is null)
                    return null;

                var lineupHtml = SendGetRequest(LineupLink.Replace("{urlKey}", urlKey));
                var squad = CreateSquadFromHtml(lineupHtml, isHome);

                return squad;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        private SquadModel CreateSquadFromHtml(string lineupHtml, bool isHome)
        {
            var squad = new SquadModel();
            var doc = new HtmlDocument();
            doc.LoadHtml(lineupHtml);

            // Starting Lineups and Substitutes
            var partsTrs = doc.DocumentNode.SelectNodes(".//div[@class='lineups-wrapper']/table[@class='parts']//tr");

            var toStart = true;
            foreach (var tr in partsTrs)
            {
                var tds = tr.SelectNodes("./td");

                if (tds is null || (tds.Count != 1 && tds.Count != 2))
                    continue;

                if (tds.Count == 1)
                {
                    switch (tds[0].InnerText)
                    {
                        case "Starting Lineups":
                            toStart = true;
                            break;
                        case "Substitutes":
                            toStart = false;
                            break;
                    }
                }
                else
                {
                    var td = tds.FirstOrDefault(x =>
                        x.Attributes["class"].Value.EndsWith($"f{(isHome ? "l" : "r")}"));

                    if (string.IsNullOrWhiteSpace(System.Net.WebUtility.HtmlDecode(td.InnerText)))
                        continue;

                    // number
                    var number = -1;
                    var timeBox = td.SelectSingleNode("./div[@class='time-box']");
                    if (timeBox != null && int.TryParse(timeBox.InnerText, out var parseResult))
                        number = parseResult;

                    // name and urlKey
                    var nameDiv = td.SelectSingleNode("./div[@class='name']");
                    if (nameDiv != null)
                    {
                        var playerName = number == -1 ? nameDiv.InnerText : $"{number}. {nameDiv.InnerText}";

                        if (toStart)
                        {
                            if (squad.StartingEleven is null)
                                squad.StartingEleven = new List<string>();

                            squad.StartingEleven.Add(playerName);
                        }
                        else
                        {
                            if (squad.Subs is null)
                                squad.Subs = new List<string>();

                            squad.Subs.Add(playerName);
                        }
                    }
                }
            }

            return squad;
        }

        private string GetUrlKey(string teamName, out bool isHome)
        {
            isHome = false;
            var result = SendGetRequest(_flashScoreUrl);
            var comps = result.Split(new[] { "ZA÷" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var comp in comps)
            {
                var m = comp.Split(new[] { "~AA" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var mDetail in m)
                {
                    var md = mDetail.Split('¬');
                    if (md.Length == 0 || !md[0].StartsWith("÷"))
                        continue;

                    var element = md.FirstOrDefault(x =>
                        (x.StartsWith("CX÷") || x.StartsWith("AF÷")) && x.EndsWith(teamName));
                    if (element is null)
                        continue;

                    isHome = element.StartsWith("CX");

                    return md[0].Replace("÷", string.Empty);
                }
            }

            return default(string);
        }

        private string SendGetRequest(string uri)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("User-Agent",
                    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.100 Safari/537.36");
                httpClient.DefaultRequestHeaders.Add("X-Fsign", "SW9D1eZo");
                httpClient.DefaultRequestHeaders.Add("Referer", "https://d.flashscore.com/x/feed/proxy-local");
                httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "cors");
                httpClient.DefaultRequestHeaders.Add("X-GeoIP", "1");
                httpClient.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");

                return httpClient.GetStringAsync(uri).GetAwaiter().GetResult();
            }
        }

        private async Task SendSticker(Message message)
        {
            Message msg1 = await _bot.SendStickerAsync(
                chatId: message.Chat,
                sticker: _stickerUrl
            );
        }

    }
}