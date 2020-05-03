using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Http;

namespace Tradier.Client
{
    public class TradierClient
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

        public async Task<string> getUserProfile()
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, "user/profile"))
            using (var response = await _httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }

        }

        public async Task<string> getBalances(string accountNumber)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, "accounts/" + accountNumber + "/balances"))
            using (var response = await _httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }

        }
    }
}
