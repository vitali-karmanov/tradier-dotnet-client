using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Tradier.Client.Exceptions;
using Tradier.Client.Helpers;
using Tradier.Client.Models.Streaming;

// ReSharper disable once CheckNamespace
namespace Tradier.Client
{
    /// <summary>
    /// The <c>Streaming</c> class
    /// </summary>
    public class Streaming
    {
        private readonly string _baseUri;
        private readonly string _baseStreamingUri = Settings.STREAMING_ENDPOINT;
        private readonly string _apiToken;

        public Streaming(string apiToken, bool useProduction)
        {
            _baseUri = useProduction
                ? Settings.PRODUCTION_ENDPOINT
                : Settings.SANDBOX_ENDPOINT;
            _apiToken = apiToken;
        }

        private TradierStreamingClient SubscribeToStream(string endpoint)
        {
            return new TradierStreamingClient(endpoint);
        }

        private TradierStreamingClient SubscribeToStream(string endpoint, string data)
        {
            return new TradierStreamingClient(endpoint, data);
        }

        public Models.Streaming.Stream CreateMarketSession()
        {
            var uri = $"{_baseUri}markets/events/session";
            var request = (HttpWebRequest)WebRequest.Create(uri);
            var requestData = "";
            var data = Encoding.ASCII.GetBytes(requestData);

            request.Method = "POST";
            request.Headers["Authorization"] = $"Bearer {_apiToken}";
            request.Accept = "application/json";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            if (!response.IsSuccessStatusCode())
            {
                throw new TradierClientException(response);
            }

            using var reader = new StreamReader(response.GetResponseStream());
            var responseString = reader.ReadToEnd();
            var result = JsonConvert.DeserializeObject<StreamRootobject>(responseString).Stream;
            return result;
        }

        public TradierStreamingClient GetStreamingQuotes(string sessionId, string symbols, StreamingFilter filter, bool lineBreak, bool validOnly, bool advancedDetails)
        {
            var endpoint = $"{_baseStreamingUri}markets/events";
            var data = $"?sessionid={sessionId}&symbols={symbols}&linebreak={lineBreak.ToString().ToLower()}&validOnly={validOnly.ToString().ToLower()}&advancedDetails={advancedDetails.ToString().ToLower()}";
            if (filter != StreamingFilter.None)
            {
                data += $"&filter={filter.ToString().ToLower()}";
            }
            var fullEndpoint = $"{endpoint}{data}";
            return SubscribeToStream(fullEndpoint);
        }

        public TradierStreamingClient PostStreamingQuotes(string sessionId, string symbols, StreamingFilter filter, bool lineBreak, bool validOnly, bool advancedDetails)
        {
            var endpoint = $"{_baseStreamingUri}markets/events";
            var data = $"sessionid={sessionId}&symbols={symbols}&linebreak={lineBreak.ToString().ToLower()}&validOnly={validOnly.ToString().ToLower()}&advancedDetails={advancedDetails.ToString().ToLower()}";
            if (filter != StreamingFilter.None)
            {
                data += $"&filter={filter.ToString().ToLower()}";
            }
            return SubscribeToStream(endpoint, data);
        }
    }
}
