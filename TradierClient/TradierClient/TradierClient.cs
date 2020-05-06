using Microsoft.Extensions.Http;
using System.Net.Http;
using Tradier.Client.Config;

// ReSharper disable once CheckNamespace
namespace Tradier.Client
{
    public class TradierClient
    {
        public Account Account { get; set; }

        public TradierClient(string apiToken, bool useSandbox = false)
        {
            HttpClient httpClient;
            TradierClientConfig tradierClientConfig = new TradierClientConfig
            {
                ApiToken = apiToken,
                AccountNumber = "VA54583566"
            };

            if (useSandbox)
            {
                tradierClientConfig.UseSandbox();
            }

            HttpClientFactory httpClientFactory = new HttpClientFactory();
            httpClientFactory.Register("TradierClient", builder => builder
            .ConfigureHttpClient(c => c.BaseAddress = tradierClientConfig.BaseUri)
            .ConfigureHttpClient(c => c.DefaultRequestHeaders.Add("Authorization", $"Bearer {tradierClientConfig.ApiToken}"))
            .ConfigureHttpClient(c => c.DefaultRequestHeaders.Add("Accept", "application/json")));

            httpClient = httpClientFactory.CreateClient("TradierClient");

            Account = new Account(httpClient, tradierClientConfig);
        }

    }
}
