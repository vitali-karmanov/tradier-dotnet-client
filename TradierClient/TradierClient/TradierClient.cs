using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Tradier.Client.Helpers;

// ReSharper disable once CheckNamespace
namespace Tradier.Client
{
    public class TradierClient
    {
        static readonly HttpClient _httpClient = new HttpClient();
        public Account Account { get; set; }
        public MarketData MarketData { get; set; }
        public Trading Trading { get; set; }
        public Watchlist Watchlist { get; set; }

        public TradierClient(string apiToken, bool useProduction = false)
        {
            Uri baseEndpoint = useProduction ? new Uri(Settings.PRODUCTION_ENDPOINT) : new Uri(Settings.SANDBOX_ENDPOINT);

            _httpClient.BaseAddress = baseEndpoint;
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiToken}");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            Requests request = new Requests(_httpClient);

            Account = new Account(request);
            MarketData = new MarketData(request);
            Trading = new Trading(request);
            Watchlist = new Watchlist(request);
        }

    }
}
