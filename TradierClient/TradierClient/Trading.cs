using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tradier.Client.Helpers;
using Tradier.Client.Models.Trading;

namespace Tradier.Client
{
    public class Trading
    {
        private readonly Requests _requests;

        public Trading(Requests requests)
        {
            _requests = requests;
        }

        public async Task<OrderStatus> PlaceOptionOrder(string accountNumber, string classOrder, string symbol, string optionSymbol, string side, string quantity, string type, string duration, string? price, string? stop)
        {
            var data = new Dictionary<string, string>
            {
                { "class", classOrder },
                { "symbol", symbol },
                { "option_symbol", optionSymbol },
                { "side", side },
                { "quantity", quantity },
                { "type", type },
                { "duration", duration },
                { "price", price },
                { "stop", stop },
            };

            var response = await _requests.PostRequest($"accounts/{accountNumber}/orders", data);
            return JsonConvert.DeserializeObject<OrderResponseRootobject>(response).OrderStatus;
        }

        public async Task<OrderStatus> ModifyOrder(string accountNumber, string orderId, string? type, string? duration, string? price, string? stop)
        {
            var data = new Dictionary<string, string>
            {
                { "type", type },
                { "duration", duration },
                { "price", price },
                { "stop", stop },
            };

            var response = await _requests.PutRequest($"accounts/{accountNumber}/orders/{orderId}", data);
            return JsonConvert.DeserializeObject<OrderResponseRootobject>(response).OrderStatus;
        }

        public async Task<OrderStatus> CancelOrder(string accountNumber, string orderId)
        {
            var response = await _requests.DeleteRequest($"accounts/{accountNumber}/orders/{orderId}");
            return JsonConvert.DeserializeObject<OrderResponseRootobject>(response).OrderStatus;
        }
    }

}
