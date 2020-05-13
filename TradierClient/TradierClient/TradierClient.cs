using System;
using Microsoft.Extensions.Http;
using System.Net.Http;
using Tradier.Client.Helpers;

// ReSharper disable once CheckNamespace
namespace Tradier.Client
{
    public class TradierClient
    {
        public Account Account { get; set; }
        public MarketData MarketData { get; set; }
        public Trading Trading { get; set; }

        public TradierClient(string apiToken, bool useProduction = false)
        {
            Uri baseEndpoint = useProduction ? new Uri(Settings.PRODUCTION_ENDPOINT) : new Uri(Settings.SANDBOX_ENDPOINT);

            HttpClientFactory httpClientFactory = new HttpClientFactory();
            httpClientFactory.Register("TradierClient", builder => builder
            .ConfigureHttpClient(c => c.BaseAddress = baseEndpoint)
            .ConfigureHttpClient(c => c.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiToken}"))
            .ConfigureHttpClient(c => c.DefaultRequestHeaders.Add("Accept", "application/json")));

            HttpClient httpClient = httpClientFactory.CreateClient("TradierClient");
            Requests request = new Requests(httpClient);

            Account = new Account(request);
            MarketData = new MarketData(request);
            Trading = new Trading(request);
        }

    }
}
