using Newtonsoft.Json;
using System.Collections.Generic;
using Tradier.Client.Helpers;

namespace Tradier.Client.Models.Watchlist
{

    public class WatchlistRootobject
    {
        [JsonProperty("watchlists")]
        public Watchlists Watchlists { get; set; }
    }

    public class Watchlists
    {
        [JsonProperty("watchlist")]
        [JsonConverter(typeof(SingleOrArrayConverter<Watchlist>))]
        public List<Watchlist> Watchlist { get; set; }
    }

    public class Watchlist
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("public_id")]
        public string PublicId { get; set; }
    }

}
