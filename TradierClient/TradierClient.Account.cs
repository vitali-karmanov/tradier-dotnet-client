using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Tradier.Client.Models;

namespace Tradier.Client
{
    public sealed partial class TradierClient
    {
        public async Task<UserProfile> getUserProfile()
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, "user/profile"))
            using (var response = await _httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<UserProfile>(content);
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
