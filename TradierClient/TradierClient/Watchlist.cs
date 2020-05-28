using System.Threading.Tasks;
using Newtonsoft.Json;
using Tradier.Client.Helpers;
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
    }
}
