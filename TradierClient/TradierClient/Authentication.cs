using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tradier.Client.Helpers;
using Tradier.Client.Models.Authentication;

// ReSharper disable once CheckNamespace
namespace Tradier.Client
{
    public class Authentication
    {
        private readonly Requests _requests;

        public Authentication(Requests requests)
        {
            _requests = requests;
        }

        public async Task<Token> CreateAccessToken(string code)
        {
            var data = new Dictionary<string, string>
            {
                { "grant_type", "authorization_code" },
                { "code", code },
            };

            var response = await _requests.PostRequest("oauth/accesstoken", data);
            return JsonConvert.DeserializeObject<Token>(response);
        }

        public async Task<Token> RefreshAccessToken(string refreshToken)
        {
            var data = new Dictionary<string, string>
            {
                { "grant_type", "refresh_token" },
                { "refresh_token", refreshToken },
            };

            var response = await _requests.PostRequest("oauth/accesstoken", data);
            return JsonConvert.DeserializeObject<Token>(response);
        }
    }
}
