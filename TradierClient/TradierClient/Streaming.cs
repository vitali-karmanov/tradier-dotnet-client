using Newtonsoft.Json;
using System.Threading.Tasks;
using Tradier.Client.Helpers;
using Tradier.Client.Models.Streaming;

namespace Tradier.Client
{
    public class Streaming
    {
        private readonly Requests _requests;

        public Streaming(Requests requests)
        {
            _requests = requests;
        }

        // TODO: We need an actual account to make sure this is working
        //public async Task<Stream> CreateMarketSession()
        //{
        //    var response = await _requests.PostRequest($"markets/events/session");
        //    return JsonConvert.DeserializeObject<StreamRootobject>(response).Stream;
        //}

        // TODO: We need to get an actual response to manage the JSON deserialization
        //public async Task<StreamResponse> GetStreamingQuotes(string sessionid, string symbols, string filter, bool lineBreak, bool validOnly, bool advancedDetails)
        //{
        //    var response = await _requests.GetRequest($"markets/events?sessionid={sessionid}&symbols={symbols}&linebreak={lineBreak}&validOnly={validOnly}&advancedDetails={advancedDetails}");
        //    return JsonConvert.DeserializeObject<StreamResponse>(response);
        //}
    }
}
