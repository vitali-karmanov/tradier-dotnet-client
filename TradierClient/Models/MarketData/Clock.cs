using System;
using Newtonsoft.Json;
using Tradier.Client.Helpers;

namespace Tradier.Client.Models.MarketData
{
    public class ClockRootobject
    {
        [JsonProperty("clock")]
        public Clock Clock { get; set; }
    }

    public class Clock
    {
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("timestamp")]
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }

        [JsonProperty("next_change")]
        public string NextChange { get; set; }

        [JsonProperty("next_state")]
        public string NextState { get; set; }
    }
}
