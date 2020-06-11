using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tradier.Client.Helpers;
using Tradier.Client.Models.Watchlist;

namespace Tradier.Client
{
    /// <summary>
    /// The <c>Watchlist</c> class
    /// </summary>
    public class Watchlist
    {
        private readonly Requests _requests;

        /// <summary>
        /// The Watchlist constructor
        /// </summary>
        public Watchlist(Requests requests)
        {
            _requests = requests;
        }

        /// <summary>
        /// Retrieve all of a users watchlists
        /// </summary>
        public async Task<Watchlists> GetWatchlists()
        {
            var response = await _requests.GetRequest("watchlists");
            return JsonConvert.DeserializeObject<WatchlistRootobject>(response).Watchlists;
        }

        /// <summary>
        /// Retrieve a specific watchlist by id
        /// </summary>
        public async Task<Watchlists> GetWatchlist(string watchlistId)
        {
            var response = await _requests.GetRequest($"watchlists/{watchlistId}");
            return JsonConvert.DeserializeObject<Watchlists>(response);
        }

        /// <summary>
        /// Create a new watchlist
        /// </summary>
        public async Task<Watchlists> CreateWatchlist(string name, string symbols)
        {
            List<string> listSymbols = symbols.Split(',').Select(x => x.Trim()).ToList();
            return await CreateWatchlist(name, listSymbols);
        }

        /// <summary>
        /// Create a new watchlist
        /// </summary>
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

        /// <summary>
        /// Update an existing watchlist
        /// </summary>
        public async Task<Watchlists> UpdateWatchlist(string watchlistId, string name, string symbols = "")
        {
            List<string> listSymbols = symbols?.Split(',').Select(x => x.Trim()).ToList();
            return await UpdateWatchlist(watchlistId, name, listSymbols);
        }

        /// <summary>
        /// Update an existing watchlist
        /// </summary>
        public async Task<Watchlists> UpdateWatchlist(string watchlistId, string name, List<string> symbols = null)
        {
            string strSymbols = String.Join(",", symbols);
            var data = new Dictionary<string, string>
            {
                { "name", name },
                { "symbols", strSymbols },
            };

            var response = await _requests.PutRequest($"watchlists/{watchlistId}", data);
            return JsonConvert.DeserializeObject<Watchlists>(response);
        }

        /// <summary>
        /// Delete a specific watchlist
        /// </summary>
        public async Task<Watchlists> DeleteWatchlist(string watchlistId)
        {
            var response = await _requests.DeleteRequest($"watchlists/{watchlistId}");
            return JsonConvert.DeserializeObject<WatchlistRootobject>(response).Watchlists;
        }

        /// <summary>
        /// Add symbols to an existing watchlist. If the symbol exists, it will be over-written
        /// </summary>
        public async Task<Watchlists> AddSymbolsToWatchlist(string watchlistId, string symbols)
        {
            List<string> listSymbols = symbols.Split(',').Select(x => x.Trim()).ToList();
            return await AddSymbolsToWatchlist(watchlistId, listSymbols);
        }

        /// <summary>
        /// Add symbols to an existing watchlist. If the symbol exists, it will be over-written
        /// </summary>
        public async Task<Watchlists> AddSymbolsToWatchlist(string watchlistId, List<string> symbols)
        {
            string strSymbols = String.Join(",", symbols);
            var data = new Dictionary<string, string>
            {
                { "symbols", strSymbols },
            };

            var response = await _requests.PostRequest($"watchlists/{watchlistId}/symbols", data);
            return JsonConvert.DeserializeObject<Watchlists>(response);
        }

        /// <summary>
        /// Remove a symbol from a specific watchlist
        /// </summary>
        public async Task<Watchlists> RemoveSymbolFromWatchlist(string watchlistId, string symbol)
        {
            var response = await _requests.DeleteRequest($"watchlists/{watchlistId}/symbols/{symbol}");
            return JsonConvert.DeserializeObject<Watchlists>(response);
        }
    }
}
