using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Tradier.Client.Helpers;

namespace Tradier.Client.Models.MarketData
{

    public class OptionChainRootobject
    {
        [JsonProperty("options")]
        public Options Options { get; set; }
    }

    public class Options
    {
        [JsonProperty("option")]
        [JsonConverter(typeof(SingleOrArrayConverter<Option>))]
        public List<Option> Option { get; set; }
    }

    public class Option
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("exch")]
        public string Exchange { get; set; }

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
        public float Bid { get; set; }

        [JsonProperty("ask")]
        public float Ask { get; set; }

        [JsonProperty("underlying")]
        public string Underlying { get; set; }

        [JsonProperty("strike")]
        public float Strike { get; set; }

        [JsonProperty("change_percentage")]
        public float? ChangePercentage { get; set; }

        [JsonProperty("average_volume")]
        public int AverageVolume { get; set; }

        [JsonProperty("last_volume")]
        public int LastVolume { get; set; }

        [JsonProperty("trade_date")]
        [JsonConverter(typeof(MillisecondsEpochConverter))]
        public DateTime TradeDate { get; set; }

        [JsonProperty("prevclose")]
        public float? PreviousClose { get; set; }

        [JsonProperty("week_52_high")]
        public float Week52High { get; set; }

        [JsonProperty("week_52_low")]
        public float Week52Low { get; set; }

        [JsonProperty("bidsize")]
        public int BidSize { get; set; }

        [JsonProperty("bidexch")]
        public string BidExchange { get; set; }

        [JsonProperty("bid_date")]
        [JsonConverter(typeof(MillisecondsEpochConverter))]
        public DateTime BidDate { get; set; }

        [JsonProperty("asksize")]
        public int AskSize { get; set; }

        [JsonProperty("askexch")]
        public string AskExchange { get; set; }

        [JsonProperty("ask_date")]
        [JsonConverter(typeof(MillisecondsEpochConverter))]
        public DateTime AskDate { get; set; }

        [JsonProperty("open_interest")]
        public int OpenInterest { get; set; }

        [JsonProperty("contract_size")]
        public int ContractSize { get; set; }

        [JsonProperty("expiration_date")]
        public string ExpirationDate { get; set; }

        [JsonProperty("expiration_type")]
        public string ExpirationType { get; set; }

        [JsonProperty("option_type")]
        public string OptionType { get; set; }

        [JsonProperty("root_symbol")]
        public string RootSymbol { get; set; }

        [JsonProperty("greeks")]
        public Greeks Greeks { get; set; }

    }

    public class Greeks
    {
        [JsonProperty("delta")]
        public float Delta { get; set; }

        [JsonProperty("gamma")]
        public float Gamma { get; set; }

        [JsonProperty("theta")]
        public float Theta { get; set; }

        [JsonProperty("vega")]
        public float Vega { get; set; }

        [JsonProperty("rho")]
        public float Rho { get; set; }

        [JsonProperty("phi")]
        public float Phi { get; set; }

        [JsonProperty("bid_iv")]
        public float BidIV { get; set; }

        [JsonProperty("mid_iv")]
        public float MidIV { get; set; }

        [JsonProperty("ask_iv")]
        public float AskIV { get; set; }

        [JsonProperty("smv_vol")]
        public float SmvIV { get; set; }

        [JsonProperty("updated_at")]
        [JsonConverter(typeof(ParseExactConverter))]
        public DateTime UpdatedAt { get; set; }
    }
}