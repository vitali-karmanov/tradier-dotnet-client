using Newtonsoft.Json;

namespace Tradier.Client.Models.Trading
{
    public class OrderResponseRootobject
    {
        [JsonProperty("order")]
        public Order Order { get; set; }
    }

    public class Order
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("partner_id")]
        public string PartnerId { get; set; }
    }
}
