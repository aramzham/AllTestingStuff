using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WpfTesting.Models;

namespace WpfTesting.Infrastructure
{
    public class OddsMarketLiveClient
    {
        private HttpClient _client = new HttpClient();
        private WebClient _webClient = new WebClient();
        private static readonly string Link = ConfigurationManager.AppSettings["Link"];
        private static readonly string BookmakersLink = ConfigurationManager.AppSettings["BookmakersLink"];
        private static readonly string MultipleBookmakerMatches = ConfigurationManager.AppSettings["MultipleBookmakerMatches"];

        private static List<long> _bookmakerIds = new List<long>();

        public async Task<string> GetBookmakers()
        {
            return await _client.GetStringAsync(BookmakersLink);
        }

        public async Task<string> GetMatchesAsync(long bookmakerId)
        {
            return await _client.GetStringAsync(Link.Replace("{id}", bookmakerId.ToString()));
        }

        public string GetMatches(long bookmakerId)
        {
            return _webClient.DownloadString(Link.Replace("{id}", bookmakerId.ToString()));
        }

        public async Task<string> GetAllBookmakerMatches()
        {
            if (_bookmakerIds.Count == 0)
            {
                var bookies = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BookmakerModel>>(await GetBookmakers());
                if (bookies != null) _bookmakerIds = bookies.Select(x => x.Id).ToList();
            }
            var url = _bookmakerIds.Aggregate(MultipleBookmakerMatches, (current, bookmakerId) => current + $"ids={bookmakerId}&").TrimEnd('&');
            return await _client.GetStringAsync(url);
        }
    }
}
