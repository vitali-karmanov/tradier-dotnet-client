using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Tradier.Client.Helpers;

namespace Tradier.Client.Models.MarketData
{

    public class SeriesRootobject
    {
        [JsonProperty("series")]
        public Series Series { get; set; }
    }

    public class Series
    {
        [JsonProperty("data")]
        public List<Datum> Data { get; set; }
    }

    public class Datum
    {
        [JsonProperty("time")]
        public DateTime Time { get; set; }

        [JsonProperty("timestamp")]
        [JsonConverter(typeof(MillisecondsEpochConverter))]
        public DateTime Timestamp { get; set; }

        [JsonProperty("price")]
        public float Price { get; set; }

        [JsonProperty("open")]
        public float Open { get; set; }

        [JsonProperty("high")]
        public float High { get; set; }

        [JsonProperty("low")]
        public float Low { get; set; }

        [JsonProperty("close")]
        public float Close { get; set; }

        [JsonProperty("volume")]
        public int Volume { get; set; }

        [JsonProperty("vwap")]
        public float Vwap { get; set; }
    }
}
