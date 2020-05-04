using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Tradier.Client.Helpers;

namespace Tradier.Client.Models
{
    public class PositionsRootobject
    {
        [JsonProperty("positions")] 
        public Positions? Positions { get; set; }
    }

    public class Positions
    {
        [JsonProperty("position")]
        [JsonConverter(typeof(SingleOrArrayConverter<Position>))]
        public List<Position>? Position { get; set; }
    }

    public class Position
    {
        [JsonProperty("cost_basis")]
        public float CostBasis { get; set; }

        [JsonProperty("date_acquired")]
        public DateTime DateAcquired { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("quantity")]
        public float Quantity { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }
    }
}