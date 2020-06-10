using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Tradier.Client.Models.MarketData
{

    public class OptionExpirationsRootobject
    {
        [JsonProperty("expirations")]
        public Expirations Expirations { get; set; }
    }

    public class Expirations
    {
        [JsonProperty("date")]
        public List<DateTime> Date { get; set; }
    }

}
