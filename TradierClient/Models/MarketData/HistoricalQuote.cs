﻿using Newtonsoft.Json;
using System.Collections.Generic;
using Tradier.Client.Helpers;

namespace Tradier.Client.Models.MarketData
{
    public class HistoricalQuotesRootobject
    {
        [JsonProperty("history")]
        public HistoricalQuotes History { get; set; }
    }

    public class HistoricalQuotes
    {
        [JsonProperty("day")]
        [JsonConverter(typeof(SingleOrArrayConverter<Day>))]
        public List<Day> Day { get; set; }
    }

    public class Day
    {
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("open")]
        public float Open { get; set; }

        [JsonProperty("high")]
        public float High { get; set; }

        [JsonProperty("low")]
        public float Low { get; set; }

        [JsonProperty("close")]
        public float Close { get; set; }

        [JsonProperty("volume")]
        public long Volume { get; set; }
    }
}