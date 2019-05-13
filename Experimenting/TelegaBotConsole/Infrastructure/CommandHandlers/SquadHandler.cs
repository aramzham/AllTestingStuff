using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using TelegaBotConsole.Infrastructure.Models;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegaBotConsole.Infrastructure.CommandHandlers
{
    public class SquadHandler : BaseCommandHandler
    {
        protected static Tuple<DateTime, SquadModel> _squad = Tuple.Create(DateTime.Now, default(SquadModel));

        private const string _livescoreUrl = "http://www.livescores.com";
        private string _stickerUrl = "https://github.com/TelegramBots/book/raw/master/src/docs/sticker-fred.webp";

        public override async Task HandleCommand(Message message)
        {
            try
            {
                var squad = await GetSquad();

                if (squad != null)
                {
                    _squad = Tuple.Create(DateTime.Now, squad);

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
            if (!(_squad?.Item2?.StartingEleven is null) && DateTime.Now - _squad.Item1 <= TimeSpan.FromMinutes(15))
                return _squad.Item2;

            try
            {
                var squad = new SquadModel();

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("accept",
                        "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3");
                    httpClient.DefaultRequestHeaders.Add("accept-language", "en-US,en;q=0.9");
                    httpClient.DefaultRequestHeaders.Add("cache-control", "max-age=0");
                    httpClient.DefaultRequestHeaders.Add("connection", "keep-alive");
                    httpClient.DefaultRequestHeaders.Add("DNT", "1");
                    httpClient.DefaultRequestHeaders.Add("Host", "www.livescores.com");
                    httpClient.DefaultRequestHeaders.Add("Referer", "http://www.livescores.com/");
                    httpClient.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
                    httpClient.DefaultRequestHeaders.Add("User-Agent",
                        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.131 Safari/537.36");

                    var result = await httpClient.GetStringAsync(_livescoreUrl);
                    var doc = new HtmlDocument();
                    doc.LoadHtml(result);

                    var isHome = true;
                    var rowGrays = doc.DocumentNode.SelectNodes(".//div[starts-with(@class,'row-gray')]");
                    if (rowGrays == null)
                        return null;

                    var arsenalA = default(HtmlNode);
                    foreach (var rowGray in rowGrays)
                    {
                        var home = rowGray.SelectSingleNode(".//div[@class='ply tright name']")?.InnerText.Trim();
                        var away = rowGray.SelectSingleNode(".//div[@class='ply name']")?.InnerText.Trim();

                        if (home == "Arsenal" || away == "Arsenal")
                        {
                            isHome = home == "Arsenal";
                            arsenalA = rowGray.SelectSingleNode(".//div[@class='sco']/a");
                            break;
                        }
                    }

                    if (arsenalA == null)
                        return null;

                    var matchResult =
                        await httpClient.GetStringAsync(
                            $"{_livescoreUrl}{arsenalA.GetAttributeValue("href", string.Empty)}");
                    doc.LoadHtml(matchResult);
                    var substitutions = doc.DocumentNode.SelectSingleNode(".//div[@data-id='substitutions']");
                    var rows = substitutions.SelectNodes(".//div[starts-with(@class,'row')]");
                    var players = new List<string>();
                    var substitutes = new List<string>();
                    foreach (var row in rows)
                    {
                        if (row.SelectNodes(".//div[@class='col-5 ']") is null &&
                            row.SelectNodes(".//div[@class='col-5 off']") is null)
                            continue;
                        if (row.InnerText.Contains("coach :"))
                            break;

                        var col5s = row.SelectNodes(".//div[@class='col-5 ']") ??
                                    row.SelectNodes(".//div[@class='col-5 off']");
                        var playerName = isHome ? col5s[0].InnerText.Trim() : col5s[1].InnerText.Trim();
                        if (row.SelectNodes(".//div[@class='col-5 ']") != null)
                            players.Add(playerName);
                        else
                            substitutes.Add(playerName);
                    }

                    squad.StartingEleven = players;

                    if (substitutes.Any())
                        squad.Subs = substitutes;

                    return squad;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
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