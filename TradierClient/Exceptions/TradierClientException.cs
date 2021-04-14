﻿using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tradier.Client.Models.Exception;

namespace Tradier.Client.Exceptions
{
    public class TradierClientException : Exception
    {
        public TradierClientException()
        {
        }

        public TradierClientException(string message)
        : base(message)
        {
        }

        public TradierClientException(HttpResponseMessage response)
        {
            Task<string> resp = response.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(resp.Result))
            {
                if (Equals(response.Content.Headers.ContentType, MediaTypeHeaderValue.Parse("application/json")))
                {
                    Fault fault = JsonConvert.DeserializeObject<FaultRootobject>(resp.Result).Fault;
                    throw new TradierClientException(fault, response);

                }

                throw new TradierClientException(resp.Result);
            }

            // if it's not any of the cases above,
            // just output the http status code exception embedded in EnsureSuccessStatusCode()
            response.EnsureSuccessStatusCode();
        }

        public TradierClientException(HttpWebResponse response)
        {
            var responseStream = response.GetResponseStream();
            string result;
            byte[] data = new byte[1024];
            using MemoryStream ms = new MemoryStream();
            int numBytesRead;
            do
            {
                numBytesRead = responseStream.Read(data, 0, data.Length);
                ms.Write(data, 0, numBytesRead);
            } while (numBytesRead >= 1024);
            result = Encoding.ASCII.GetString(ms.ToArray(), 0, (int)ms.Length);

            if (!string.IsNullOrEmpty(result))
            {
                throw new TradierClientException(result);
            }
        }

        public TradierClientException(Fault fault, HttpResponseMessage response)
        {
            throw new Exception(responseBuilder(response, fault));
        }

        public TradierClientException(string message, Exception innerException)
        : base(message, innerException)
        {
        }

        private string responseBuilder(HttpResponseMessage response, Fault fault = null)
        {
            var messageStream = response.Content.ReadAsStreamAsync().Result;
            var messageField = (fault != null ? fault.FaultString : messageStream.ToString());
            string messageBuilt = $"IsSuccessStatusCode: {response.IsSuccessStatusCode}\n" +
                             $"Reason: {response.ReasonPhrase}\n" +
                             $"StatusCode: {response.StatusCode.ToString()}\n" +
                             $"Message: {messageField}";

            return messageBuilt;
        }
    }
}