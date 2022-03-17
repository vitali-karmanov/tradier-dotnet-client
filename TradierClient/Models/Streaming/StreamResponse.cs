using Newtonsoft.Json;

namespace Tradier.Client.Models.Streaming
{
    public class StreamResponse
    {
        public QuoteStream QuoteStream { get; set; }
        public TradeStream TradeStream { get; set; }
        public SummaryStream SummaryStream { get; set; }
        public TimeSaleStream TimeSaleStream { get; set; }
        public TradeXStream TradeXStream { get; set; }
    }

    public class QuoteStream
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("bid")]
        public float Bid { get; set; }

        [JsonProperty("bidsz")]
        public int BidSize { get; set; }

        [JsonProperty("bidexch")]
        public string BidExchange { get; set; }

        [JsonProperty("biddate")]
        public string BidDate { get; set; }

        [JsonProperty("ask")]
        public float Ask { get; set; }

        [JsonProperty("asksz")]
        public int AskSize { get; set; }

        [JsonProperty("askexch")]
        public string AskExchange { get; set; }

        [JsonProperty("askdate")]
        public string AskDate { get; set; }
    }

    public class TradeStream
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("exch")]
        public string Exchange { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("cvol")]
        public string CumulativeVolume { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("last")]
        public string Last { get; set; }
    }
    
    public class SummaryStream
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("open")]
        public string Open { get; set; }

        [JsonProperty("high")]
        public string High { get; set; }

        [JsonProperty("low")]
        public string Low { get; set; }

        [JsonProperty("prevClose")]
        public string PreviousClose { get; set; }
    }

    public class TimeSaleStream
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("exch")]
        public string Exchange { get; set; }

        [JsonProperty("bid")]
        public string Bid { get; set; }

        [JsonProperty("ask")]
        public string Ask { get; set; }

        [JsonProperty("last")]
        public string Last { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("seq")]
        public int Sequence { get; set; }

        /// <summary>
        /// Event Flag Reference: https://docs.dxfeed.com/misc/dxFeed_TimeAndSale_Sale_Conditions.htm
        /// </summary>
        [JsonProperty("flag")]
        public string Flag { get; set; }

        [JsonProperty("cancel")]
        public bool IsCancel { get; set; }

        [JsonProperty("correction")]
        public bool IsCorrection { get; set; }

        [JsonProperty("session")]
        public string Session { get; set; }
    }

    public class TradeXStream
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("exch")]
        public string Exchange { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("cvol")]
        public string CumulativeVolume { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("last")]
        public string Last { get; set; }
    }
}
