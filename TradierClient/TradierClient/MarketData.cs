using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using Tradier.Client.Helpers;
using Tradier.Client.Models.Account;
using Tradier.Client.Models.MarketData;

// ReSharper disable once CheckNamespace
namespace Tradier.Client
{
    public class MarketData
    {
        private readonly Requests _requests;

        public MarketData(Requests requests)
        {
            _requests = requests;
        }

        public async Task<Options> GetOptionChain(string symbol, string expiration, bool greeks = false)
        {
            var response = await _requests.GetRequest($"markets/options/chains?symbol={symbol}&expiration={expiration}&greeks={greeks}");
            return JsonConvert.DeserializeObject<OptionChainRootobject>(response).Options;
        }

    }
}