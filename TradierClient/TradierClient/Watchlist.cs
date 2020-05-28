using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tradier.Client.Helpers;
using Tradier.Client.Models.MarketData;
using Tradier.Client.Models.Watchlist;

namespace Tradier.Client
{
    public class Watchlist
    {
        private readonly Requests _requests;

        public Watchlist(Requests requests)
        {
            _requests = requests;
        }

        public async Task<Watchlists> GetWatchlists()
        {
            var response = await _requests.GetRequest("watchlists");
            return JsonConvert.DeserializeObject<WatchlistRootobject>(response).Watchlists;
        }
        
        public async Task<Watchlists> GetWatchlist(string watchlistId)
        {
            var response = await _requests.GetRequest($"watchlists/{watchlistId}");
            return JsonConvert.DeserializeObject<Watchlists>(response);
        }

        public async Task<Watchlists> CreateWatchlist(string name, string symbols)
        {
            List<string> listSymbols = symbols.Split(',').Select(x => x.Trim()).ToList();
            return await CreateWatchlist(name, listSymbols);
        }

        public async Task<Watchlists> CreateWatchlist(string name, List<string> symbols)
        {
            string strSymbols = String.Join(",", symbols);
            var data = new Dictionary<string, string>
            {
                { "name", name },
                { "symbols", strSymbols },
            };

            var response = await _requests.PostRequest($"watchlists", data);
            return JsonConvert.DeserializeObject<Watchlists>(response);
        }
    }
}
