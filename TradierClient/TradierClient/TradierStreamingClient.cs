using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tradier.Client.Models.Streaming;

// ReSharper disable once CheckNamespace
namespace Tradier.Client
{
    /// <summary>
    /// The <c>Streaming</c> class
    /// </summary>
    public class TradierStreamingClient : IDisposable
    {
        private readonly string _uri;
        private readonly System.IO.Stream _stream;
        private readonly StreamReader _streamReader;
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        /// <summary>
        /// The Streaming constructor
        /// </summary>
        public TradierStreamingClient(string endpoint, string content = null)
        {
            _jsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            _uri = endpoint;
            var request = (HttpWebRequest)WebRequest.Create(_uri);

            request.Accept = "application/json";

            if (content == null)
            {
                request.Method = HttpMethod.Get.ToString().ToUpper();
            }
            else
            {
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = HttpMethod.Post.ToString().ToUpper();
                var data = Encoding.ASCII.GetBytes(content);
                request.ContentLength = data.Length;
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }

            var response = (HttpWebResponse)request.GetResponse();

            _stream = response.GetResponseStream();
            _streamReader = new StreamReader(_stream);

            onOpened(this, new StateChangedEventArgs(ReadyState.Open));
        }

        public async Task StartAsync()
        {
            while (!_streamReader.EndOfStream)
            {
                string currentLine;
                byte[] data = new byte[1024];
                using MemoryStream ms = new MemoryStream();
                int numBytesRead;
                do
                {
                    numBytesRead = await _stream.ReadAsync(data, 0, data.Length);
                    ms.Write(data, 0, numBytesRead);
                } while (numBytesRead >= 1024);
                currentLine = Encoding.ASCII.GetString(ms.ToArray(), 0, (int)ms.Length);

                var me = new MessageEvent(_uri, currentLine);
                Evt_MessageReceived(this, new MessageReceivedEventArgs(me));
            }
        }

        #region Public Events
        /// <summary>
        /// Occurs when the connection to the server has been opened.
        /// </summary>
        public event EventHandler<StateChangedEventArgs> Opened;
        void onOpened(object s, StateChangedEventArgs e) => Opened?.Invoke(s, e);
        /// <summary>
        /// Occurs when the connection to the server has been closed.
        /// </summary>
        public event EventHandler<StateChangedEventArgs> Closed;
        void onClosed(object s, StateChangedEventArgs e) => Closed?.Invoke(s, e);
        ///// <summary>
        ///// Occurs when a message has been received from the server.
        ///// </summary>
        //public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public event EventHandler<StreamResponse> MessageReceived;
        /// <summary>
        /// Occurs when an error has happened during processing.
        /// </summary>
        public event EventHandler<ExceptionEventArgs> Error;
        void onError(object s, ExceptionEventArgs e) => Error?.Invoke(s, e);
        #endregion Public Events

        private void Evt_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            var content = e.Message.Data;
            try
            {
                var splitContent = content.Split(new string[] { "}{", "}\n{" }, StringSplitOptions.None);
                var result = new StreamResponse();

                QuoteStream quoteStream = null;
                TradeStream tradeStream = null;
                SummaryStream summaryStream = null;
                TimeSaleStream timeSaleStream = null;
                TradeXStream tradeXStream = null;

                for (var i = 0; i < splitContent.Length; i++)
                {
                    var item = splitContent[i];
                    if (i > 0) item = "{" + item;
                    if (i < splitContent.Length - 1) item += "}";
                    if (!item.Contains("}"))
                    {
                        Console.WriteLine("Error");
                    }

                    if (item.Contains("\"type\":\"quote\""))
                    {
                        quoteStream = JsonConvert.DeserializeObject<QuoteStream>(item, _jsonSerializerSettings);
                    }
                    if (item.Contains("\"type\":\"trade\""))
                    {
                        tradeStream = JsonConvert.DeserializeObject<TradeStream>(item, _jsonSerializerSettings);
                    }
                    if (item.Contains("\"type\":\"summary\""))
                    {
                        summaryStream = JsonConvert.DeserializeObject<SummaryStream>(item, _jsonSerializerSettings);
                    }
                    if (item.Contains("\"type\":\"timesale\""))
                    {
                        timeSaleStream = JsonConvert.DeserializeObject<TimeSaleStream>(item, _jsonSerializerSettings);
                    }
                    if (item.Contains("\"type\":\"tradex\""))
                    {
                        tradeXStream = JsonConvert.DeserializeObject<TradeXStream>(item, _jsonSerializerSettings);
                    }
                }

                result.QuoteStream = quoteStream;
                result.TradeStream = tradeStream;
                result.SummaryStream = summaryStream;
                result.TimeSaleStream = timeSaleStream;
                result.TradeXStream = tradeXStream;

                MessageReceived?.Invoke(this, result);
            }
            catch (JsonException ex)
            {
                onError(this, new ExceptionEventArgs(ex));
            }
        }

        public void Dispose()
        {
            _stream.Dispose();
            _streamReader.Dispose();
            onClosed(this, new StateChangedEventArgs(ReadyState.Closed));
        }
    }
}
