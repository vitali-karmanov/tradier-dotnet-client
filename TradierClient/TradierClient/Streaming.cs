using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tradier.Client.Helpers;
using Tradier.Client.Models.Streaming;
using Tradier.Client.Models.Trading;

namespace Tradier.Client
{
    public class Streaming
    {
        private readonly Requests _requests;

        public Streaming(Requests requests)
        {
            _requests = requests;
        }

        public async Task<Stream> CreateMarketSession()
        {
            var response = await _requests.PostRequest($"markets/events/session");
            return JsonConvert.DeserializeObject<StreamRootobject>(response).Stream;
        }
    }
}
