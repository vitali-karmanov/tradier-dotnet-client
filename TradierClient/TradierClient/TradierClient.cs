using System;
using System.Net.Http;
using Tradier.Client.Helpers;

// ReSharper disable once CheckNamespace
namespace Tradier.Client
{
    public class TradierClient
    {
        public Authentication Authentication { get; set; }
        public Account Account { get; set; }
        public MarketData MarketData { get; set; }
        public Trading Trading { get; set; }
        public Watchlist Watchlist { get; set; }

        // TODO: We need a non-sandbox account to make this work
        //public Streaming Streaming { get; set; }

        public TradierClient(string apiToken, bool useProduction = false)
           : this(new HttpClient(), apiToken, useProduction)
        {
        }

        public TradierClient(HttpClient httpClient, string apiToken, bool useProduction = false)
        {
            Uri baseEndpoint = useProduction ? new Uri(Settings.PRODUCTION_ENDPOINT) : new Uri(Settings.SANDBOX_ENDPOINT);

            httpClient.BaseAddress = baseEndpoint;
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiToken}");
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            Requests request = new Requests(httpClient);

            Authentication = new Authentication(request);
            Account = new Account(request);
            MarketData = new MarketData(request);
            Trading = new Trading(request);
            Watchlist = new Watchlist(request);

            // TODO: We need a non-sandbox account to make this work
            //Streaming = new Streaming(request);
        }

    }
}
