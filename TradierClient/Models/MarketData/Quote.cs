using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Tradier.Client.Helpers;

namespace Tradier.Client.Models.MarketData
{
    public class QuoteRootobject
    {
        [JsonProperty("quotes")]
        public Quotes Quotes { get; set; }
    }

    public class Quotes
    {
        [JsonProperty("quote")]
        [JsonConverter(typeof(SingleOrArrayConverter<Quote>))]
        public List<Quote> Quote { get; set; }
    }

    public class Quote
	{
		public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		[JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("exch")]
        public string Exch { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("last")]
        public float? Last { get; set; }

        [JsonProperty("change")]
        public float? Change { get; set; }

        [JsonProperty("volume")]
        public int Volume { get; set; }

        [JsonProperty("open")]
        public float? Open { get; set; }

        [JsonProperty("high")]
        public float? High { get; set; }

        [JsonProperty("low")]
        public float? Low { get; set; }

        [JsonProperty("close")]
        public float? Close { get; set; }

        [JsonProperty("bid")]
        public float? Bid { get; set; }

        [JsonProperty("ask")]
        public float? Ask { get; set; }

        [JsonProperty("change_percentage")]
        public float? ChangePercentage { get; set; }

        [JsonProperty("average_volume")]
        public int AverageVolume { get; set; }

        [JsonProperty("last_volume")]
        public int LastVolume { get; set; }

        [JsonProperty("trade_date")]
        public long TradeDate { get; set; }

		[JsonIgnore]
		public DateTime TradeDateTime => UnixEpoch.AddMilliseconds(TradeDate);

        [JsonProperty("prevclose")]
        public float? Prevclose { get; set; }

        [JsonProperty("week_52_high")]
        public float Week52High { get; set; }

        [JsonProperty("week_52_low")]
        public float Week52Low { get; set; }

        [JsonProperty("bidsize")]
        public int Bidsize { get; set; }

        [JsonProperty("bidexch")]
        public string Bidexch { get; set; }

        [JsonProperty("bid_date")]
        public long BidDate { get; set; }

		[JsonIgnore]
		public DateTime BidDateTime => UnixEpoch.AddMilliseconds(BidDate);

		[JsonProperty("asksize")]
        public int Asksize { get; set; }

        [JsonProperty("askexch")]
        public string Askexch { get; set; }

        [JsonProperty("ask_date")]
        public long AskDate { get; set; }

		[JsonIgnore]
		public DateTime AskDateTime => UnixEpoch.AddMilliseconds(AskDate);

		[JsonProperty("root_symbols")]
        public string RootSymbols { get; set; }

        [JsonProperty("underlying")]
        public string Underlying { get; set; }

        [JsonProperty("strike")]
        public float Strike { get; set; }

        [JsonProperty("open_interest")]
        public int OpenInterest { get; set; }

        [JsonProperty("contract_size")]
        public int ContractSize { get; set; }

        [JsonProperty("expiration_date")]
        public string ExpirationDate { get; set; }

		[JsonIgnore]
		public DateTime ExpirationDateTime => DateTime.Parse(ExpirationDate);

		[JsonProperty("expiration_type")]
        public string ExpirationType { get; set; }

        [JsonProperty("option_type")]
        public string OptionType { get; set; }

        [JsonProperty("root_symbol")]
        public string RootSymbol { get; set; }

		[JsonProperty("greeks")]
		public Greeks Greeks { get; set; }
    }
}
