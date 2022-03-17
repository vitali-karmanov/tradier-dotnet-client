using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Tradier.Client.Models.Account
{

    public class HistoryRootobject
    {
        [JsonProperty("history")]
        public History History { get; set; }
    }

    public class History
    {
        [JsonProperty("event")]
        public List<Event> Event { get; set; }
    }

    public class Event
    {
        [JsonProperty("amount")]
        public float Amount { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("trade")]
        public Trade Trade { get; set; }

        [JsonProperty("adjustment")]
        public Adjustment Adjustment { get; set; }

        [JsonProperty("option")]
        public Option Option { get; set; }

        [JsonProperty("journal")]
        public Journal Journal { get; set; }
    }

    public class Journal
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("quantity")]
        public float Quantity { get; set; }
    }

    public class Trade : Journal
    {
        [JsonProperty("commission")]
        public float Commission { get; set; }

        [JsonProperty("price")]
        public float Price { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("trade_type")]
        public string TradeType { get; set; }
    }

    public class Adjustment : Journal
    {
    }

    public class Option : Journal
    {
        [JsonProperty("option_type")]
        public string OptionType { get; set; }
    }
}
