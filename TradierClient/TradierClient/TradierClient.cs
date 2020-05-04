using Microsoft.Extensions.Http;
using System.Net.Http;
using Tradier.Client.Config;

// ReSharper disable once CheckNamespace
namespace Tradier.Client
{
    public sealed partial class TradierClient
    {
        private readonly HttpClient _httpClient;
        private readonly TradierClientConfig _config = new TradierClientConfig();

        public TradierClient(string apiToken, bool useSandbox = false)
        {
            _config.ApiToken = apiToken;
            if (useSandbox)
            {
                _config.UseSandbox();
            }

            HttpClientFactory httpClientFactory = new HttpClientFactory();
            httpClientFactory.Register("TradierClient", builder => builder
            .ConfigureHttpClient(c => c.BaseAddress = _config.BaseUri)
            .ConfigureHttpClient(c => c.DefaultRequestHeaders.Add("Authorization", $"Bearer {_config.ApiToken}"))
            .ConfigureHttpClient(c => c.DefaultRequestHeaders.Add("Accept", "application/json")));

            _httpClient = httpClientFactory.CreateClient("TradierClient");

        }

    }
}
