using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Tradier.Client.Helpers
{
    public class Requests
    {
        private readonly HttpClient _httpClient;

        public Requests(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetContent(Uri uri)
        {
            return await GetContent(uri.ToString());
        }

        public async Task<string> GetContent(string uri)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, uri);
            using var response = await _httpClient.SendAsync(request);
            {
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                content = content.Replace("\"null\"", "null");

                return content;
            }
        }

        public async Task<string> PostContent(string uri, Dictionary<string, string> values)
        {
            using var response = await _httpClient.PostAsync(uri, new FormUrlEncodedContent(values));
            {
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                content = content.Replace("\"null\"", "null");

                return content;
            }
        }
    }
}
