using Newtonsoft.Json;

namespace Tradier.Client.Models.Exception
{
    public class FaultRootobject
    {
        [JsonProperty("fault")]
        public Fault Fault { get; set; }
    }

    public class Fault
    {
        [JsonProperty("faultstring")]
        public string FaultString { get; set; }
        [JsonProperty("detail")]
        public Detail Detail { get; set; }
    }

    public class Detail
    {
        [JsonProperty("errorcode")]
        public string ErrorCode { get; set; }
    }

}
