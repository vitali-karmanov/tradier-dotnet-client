using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tradier.Client.Helpers;
using Tradier.Client.Models.Authentication;

// ReSharper disable once CheckNamespace
namespace Tradier.Client
{
    /// <summary>
    /// The <c>Authentication</c> class
    /// </summary>
    public class Authentication
    {
        private readonly Requests _requests;
        
        /// <summary>
        /// The Authentication constructor
        /// </summary>
        public Authentication(Requests requests)
        {
            _requests = requests;
        }

        /// <summary>
        /// You can obtain an access token by exchanging an authorization code
        /// </summary>
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

        /// <summary>
        /// You will obtain a refresh token in the same response as an access token
        /// </summary>
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
