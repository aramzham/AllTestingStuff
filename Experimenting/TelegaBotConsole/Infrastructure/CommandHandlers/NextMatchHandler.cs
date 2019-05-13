using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegaBotConsole.Infrastructure.CommandHandlers
{
    public class NextMatchHandler : BaseCommandHandler
    {
        private const string _arsenalUrl = "https://www.arsenal.com";

        public override async Task HandleCommand(Message message)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var result = await httpClient.GetStringAsync(_arsenalUrl);
                    var doc = new HtmlDocument();
                    doc.LoadHtml(result);

                    var liveMatchBlock = doc.DocumentNode.SelectSingleNode(".//div[@id='block-livematch']");
                    // there is a live match
                    if (liveMatchBlock != null && !string.IsNullOrWhiteSpace(liveMatchBlock.InnerText))
                    {
                        await _bot.SendTextMessageAsync(
                            chatId: message.Chat,
                            text: "Match is live"
                        );

                        var figcaptions = liveMatchBlock.SelectNodes(".//figcaption[@class='takeover__match__team__name']");
                        var scoreDiv = liveMatchBlock.SelectSingleNode(".//div[@class='takeover__match__info']");
                        var scoreText = scoreDiv.InnerText.Replace("\n",string.Empty).Trim();
                        var resultText = $"{figcaptions[0].InnerText} {scoreText} {figcaptions[1].InnerText}";

                        await _bot.SendTextMessageAsync(
                            chatId: message.Chat,
                            text: resultText
                        );
                    }
                    // if no live match => get next match
                    else
                    {
                        var nextMatchBlock = doc.DocumentNode.SelectSingleNode(".//*[@id='block-views-block-fixtures-page-block-4']");

                        var eventInfoDate = nextMatchBlock.SelectSingleNode(".//div[@class='event-info__date']");
                        var dateText = eventInfoDate.InnerText;

                        var eventInfoVenue = nextMatchBlock.SelectSingleNode(".//div[@class='event-info__venue']");
                        var venueText = eventInfoVenue.InnerText;

                        var teamCrests = nextMatchBlock.SelectNodes(".//div[@class='team-crest__name-value']");
                        var innerTexts = teamCrests.Select(x => x.InnerText).ToList();

                        var crest = nextMatchBlock.SelectSingleNode(".//figure/img[not(contains(@alt, 'Arsenal'))]");
                        var crestUrl = crest.GetAttributeValue("src", string.Empty);

                        var fixtureInfo = nextMatchBlock.SelectSingleNode(".//footer[@class='card__footer']//a");
                        var fixtureUrl = fixtureInfo.GetAttributeValue("href", string.Empty);

                        var text = $"{innerTexts[0]} vs {innerTexts[1]} | {dateText} | {venueText}";

                        Message mes = await _bot.SendPhotoAsync(
                            chatId: message.Chat,
                            photo: $"{_arsenalUrl}{crestUrl}",
                            caption: $"<b>{text}</b>. <i>Source</i>: <a href=\"{_arsenalUrl}{fixtureUrl}\">Official</a>",
                            parseMode: ParseMode.Html
                        );

                        //await _bot.SendTextMessageAsync(
                        //    chatId: message.Chat,
                        //    text: text
                        //);
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