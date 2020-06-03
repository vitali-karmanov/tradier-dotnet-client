using Newtonsoft.Json;
using System;
using Tradier.Client.Helpers;

namespace Tradier.Client.Models.Authentication
{

    public class Token
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("refresh_token")]
        public int RefreshToken { get; set; }

        [JsonProperty("expires_in")]
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime ExpiresIn { get; set; }

        [JsonProperty("issued_at")]
        public DateTime IssuedAt { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}