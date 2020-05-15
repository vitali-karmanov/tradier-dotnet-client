using Newtonsoft.Json;
using System.Collections.Generic;

namespace Tradier.Client.Models.MarketData
{

    public class SecuritiesRootobject
    {
        [JsonProperty("securities")]
        public Securities Securities { get; set; }
    }

    public class Securities
    {
        [JsonProperty("security")]
        public List<Security> Security { get; set; }
    }

    public class Security
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("exchange")]
        public string Exchange { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

}
