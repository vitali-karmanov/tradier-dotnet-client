using Newtonsoft.Json;

namespace Tradier.Client.Models.Trading
{
    public interface IOrder
    {
        [JsonProperty("status")]
        public string Status { get; set; }
    }

}
