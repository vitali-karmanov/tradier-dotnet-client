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
        public string type { get; set; }
        public string symbol { get; set; }
        public float bid { get; set; }
        public int bidsz { get; set; }
        public string bidexch { get; set; }
        public string biddate { get; set; }
        public float ask { get; set; }
        public int asksz { get; set; }
        public string askexch { get; set; }
        public string askdate { get; set; }
    }

    public class TradeStream
    {
        public string type { get; set; }
        public string symbol { get; set; }
        public string exch { get; set; }
        public string price { get; set; }
        public string size { get; set; }
        public string cvol { get; set; }
        public string date { get; set; }
        public string last { get; set; }
    }
    
    public class SummaryStream
    {
        public string type { get; set; }
        public string symbol { get; set; }
        public string open { get; set; }
        public string high { get; set; }
        public string low { get; set; }
        public string prevClose { get; set; }
    }

    public class TimeSaleStream
    {
        public string type { get; set; }
        public string symbol { get; set; }
        public string exch { get; set; }
        public string bid { get; set; }
        public string ask { get; set; }
        public string last { get; set; }
        public string size { get; set; }
        public string date { get; set; }
        public int seq { get; set; }
        public string flag { get; set; }
        public bool cancel { get; set; }
        public bool correction { get; set; }
        public string session { get; set; }
    }
    public class TradeXStream
    {
        public string type { get; set; }
        public string symbol { get; set; }
        public string exch { get; set; }
        public string price { get; set; }
        public string size { get; set; }
        public string cvol { get; set; }
        public string date { get; set; }
        public string last { get; set; }
    }
}
