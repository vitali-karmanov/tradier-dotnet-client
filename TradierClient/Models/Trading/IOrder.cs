using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tradier.Client.Models.Trading
{
    public interface IOrder
    {
        [JsonProperty("status")]
        public string Status { get; set; }
    }

}
