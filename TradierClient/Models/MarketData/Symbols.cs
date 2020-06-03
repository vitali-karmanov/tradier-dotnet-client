using Newtonsoft.Json;
using System.Collections.Generic;

namespace Tradier.Client.Models.MarketData
{

    public class SymbolsRootobject
    {
        [JsonProperty("symbols")]
        public List<Symbol> Symbols { get; set; }
    }

    public class Symbol
    {
        [JsonProperty("rootSymbol")]
        public string RootSymbol { get; set; }

        [JsonProperty("options")]
        public List<string> Options { get; set; }
    }

}
