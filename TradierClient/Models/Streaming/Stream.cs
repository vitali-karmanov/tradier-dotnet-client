using Newtonsoft.Json;

namespace Tradier.Client.Models.Streaming
{

    public class StreamRootobject
    {
        [JsonProperty("stream")]
        public Stream Stream { get; set; }
    }

    public class Stream
    {
        [JsonProperty("url")]
        public string Url { get; set; }
        
        [JsonProperty("sessionid")]
        public string SessionId { get; set; }
    }

}
