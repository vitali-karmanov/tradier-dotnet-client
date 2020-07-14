using Newtonsoft.Json;
using System;

namespace Tradier.Client.Models.Trading
{
    public class OrderResponseRootobject
    {
        [JsonProperty("order")]
        public OrderReponse OrderReponse { get; set; }
    }

    public class OrderReponse : IOrder
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("partner_id")]
        public string PartnerId { get; set; }
    }
}
