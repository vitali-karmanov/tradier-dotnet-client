using Newtonsoft.Json;
using System.Collections.Generic;

namespace Tradier.Client.Models.MarketData
{

    public class StrikeRootobject
    {
        [JsonProperty("strikes")]
        public Strikes Strikes { get; set; }
    }

    public class Strikes
    {
        [JsonProperty("strike")]
        public List<float> Strike { get; set; }
    }

}
