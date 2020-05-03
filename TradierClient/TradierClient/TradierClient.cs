using System;
using System.Net.Http;
using Microsoft.Extensions.Http;

namespace Tradier.Client
{
    public sealed partial class TradierClient
    {
        private readonly HttpClient _httpClient;

        public TradierClient(string accessToken)
        {
            HttpClientFactory httpClientFactory = new HttpClientFactory();
            httpClientFactory.Register("sandbox", builder => builder
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://sandbox.tradier.com/v1/"))
            .ConfigureHttpClient(c => c.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken))
            .ConfigureHttpClient(c => c.DefaultRequestHeaders.Add("Accept", "application/json")));

            _httpClient = httpClientFactory.CreateClient("sandbox");
        }
    }
}
