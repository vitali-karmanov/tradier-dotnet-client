using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Tradier.Client.Models.Account
{

    public class GainLossRootobject
    {
        [JsonProperty("gainloss")]
        public GainLoss GainLoss { get; set; }
    }

    public class GainLoss
    {
        [JsonProperty("closed_position")]
        public List<ClosedPosition> ClosedPosition { get; set; }
    }

    public class ClosedPosition
    {
        [JsonProperty("close_date")]
        public DateTime CloseDate { get; set; }
        
        [JsonProperty("cost")]
        public float Cost { get; set; }
        
        [JsonProperty("gain_loss")]
        public float GainLoss { get; set; }
        
        [JsonProperty("gain_loss_percent")]
        public float GainLossPercent { get; set; }
        
        [JsonProperty("open_date")]
        public DateTime OpenDate { get; set; }
        
        [JsonProperty("proceeds")]
        public float Proceeds { get; set; }
        
        [JsonProperty("quantity")]
        public float Quantity { get; set; }
        
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
        
        [JsonProperty("term")]
        public int Term { get; set; }
    }

}
