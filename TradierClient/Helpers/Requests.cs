using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Tradier.Client.Helpers
{
    public class Requests<T>
    {
        private readonly HttpClient _httpClient;

        public Requests(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> GetDeserialized(Uri uri)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, uri);
            using var response = await _httpClient.SendAsync(request);
            {
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                content = content.Replace("\"null\"", "null");

                return JsonConvert.DeserializeObject<T>(content);
            }
        }

        public async Task<T> GetDeserialized(string uri)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, uri);
            using var response = await _httpClient.SendAsync(request);
            {
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                content = content.Replace("\"null\"", "null");

                return JsonConvert.DeserializeObject<T>(content);
            }
        }
    }
}
