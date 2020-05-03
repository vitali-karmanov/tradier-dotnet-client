using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Tradier.Client.Models;

namespace Tradier.Client
{
    public sealed partial class TradierClient
    {
        public async Task<Profile> GetUserProfile()
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, "user/profile"))
            using (var response = await _httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ProfileRootObject>(content).Profile;
            }
        }

        public async Task<Balances> GetBalances(string accountNumber)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, "accounts/" + accountNumber + "/balances"))
            using (var response = await _httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<BalanceRootObject>(content).Balances;
            }
        }
    }
}
