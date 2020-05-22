using Newtonsoft.Json;
using System;

namespace Tradier.Client.Models.Trading
{
    public class OrderPreviewResponseRootobject
    {
        [JsonProperty("order")]
        public OrderPreviewResponse OrderPreviewResponse { get; set; }
    }

    public class OrderPreviewResponse : IOrder
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("commission")]
        public float? Commision { get; set; }

        [JsonProperty("cost")]
        public float? Cost { get; set; }

        [JsonProperty("fees")]
        public float? Fees { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("quantity")]
        public int? Quantity { get; set; }

        [JsonProperty("side")]
        public string Side { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("duration")]
        public string Duration { get; set; }

        [JsonProperty("result")]
        public bool? Result { get; set; }

        [JsonProperty("order_cost")]
        public float? OrderCost { get; set; }

        [JsonProperty("margin_change")]
        public float? MarginChange { get; set; }

        [JsonProperty("request_date")]
        public DateTime? RequestDate { get; set; }

        [JsonProperty("extended_hours")]
        public bool? ExtendedHours { get; set; }

        [JsonProperty("class")]
        public string ClassOrder { get; set; }

        [JsonProperty("strategy")]
        public string Strategy { get; set; }

        [JsonProperty("day_trades")]
        public int? DayTrades { get; set; }
    }
}
