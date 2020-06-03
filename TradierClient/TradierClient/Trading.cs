using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tradier.Client.Helpers;
using Tradier.Client.Models.Trading;

// ReSharper disable once CheckNamespace
namespace Tradier.Client
{
    public class Trading
    {
        private readonly Requests _requests;

        public Trading(Requests requests)
        {
            _requests = requests;
        }

        public async Task<IOrder> PlaceOptionOrder(string accountNumber, string symbol, string optionSymbol, string side, int quantity, string type, string duration, double? price = null, double? stop = null, string preview = null)
        {
            var data = new Dictionary<string, string>
            {
                { "class", "option" },
                { "symbol", symbol },
                { "option_symbol", optionSymbol },
                { "side", side },
                { "quantity", quantity.ToString() },
                { "type", type },
                { "duration", duration },
                { "price", price.ToString() },
                { "stop", stop.ToString() },
                { "preview", preview }
            };

            var response = await _requests.PostRequest($"accounts/{accountNumber}/orders", data);

            if (preview != null && preview.Equals("true"))
            {
                return JsonConvert.DeserializeObject<OrderPreviewResponseRootobject>(response).OrderPreviewResponse;

            }
            else
            {
                return JsonConvert.DeserializeObject<OrderResponseRootobject>(response).OrderReponse;
            }
        }

        public async Task<OrderReponse> PlaceMultilegOrder(string accountNumber, string symbol, string type, string duration, List<Tuple<string, string, int>> legs, double? price = null)
        {
            var data = new Dictionary<string, string>
            {
                { "class", "multileg" },
                { "symbol", symbol },
                { "type", type },
                { "duration", duration },
                { "price", price.ToString() }
            };

            int index = 0;

            foreach (var leg in legs)
            {
                data.Add($"option_symbol[{index}]", leg.Item1);
                data.Add($"side[{index}]", leg.Item2);
                data.Add($"quantity[{index}]", leg.Item3.ToString());

                index++;
            }

            var response = await _requests.PostRequest($"accounts/{accountNumber}/orders", data);
            return JsonConvert.DeserializeObject<OrderResponseRootobject>(response).OrderReponse;
        }

        public async Task<OrderReponse> ModifyOrder(string accountNumber, string orderId, string type = null, string duration = null, string price = null, string stop = null)
        {
            var data = new Dictionary<string, string>
            {
                { "type", type },
                { "duration", duration },
                { "price", price },
                { "stop", stop },
            };

            var response = await _requests.PutRequest($"accounts/{accountNumber}/orders/{orderId}", data);
            return JsonConvert.DeserializeObject<OrderResponseRootobject>(response).OrderReponse;
        }

        public async Task<OrderReponse> CancelOrder(string accountNumber, string orderId)
        {
            var response = await _requests.DeleteRequest($"accounts/{accountNumber}/orders/{orderId}");
            return JsonConvert.DeserializeObject<OrderResponseRootobject>(response).OrderReponse;
        }

        public async Task<OrderReponse> PlaceEquityOrder(string accountNumber, string symbol, string side, int quantity, string type, string duration, double? price = null, double? stop = null)
        {
            var data = new Dictionary<string, string>
            {
                { "account_id", accountNumber },
                { "class", "equity" },
                { "symbol", symbol },
                { "side", side },
                { "quantity", quantity.ToString()},
                { "type", type },
                { "duration", duration },
                { "price", price.ToString() },
                { "stop", stop.ToString() },
            };

            var response = await _requests.PostRequest($"accounts/{accountNumber}/orders", data);
            return JsonConvert.DeserializeObject<OrderResponseRootobject>(response).OrderReponse;
        }

        public async Task<OrderReponse> PlaceComboOrder(string accountNumber, string symbol, string type, string duration, List<Tuple<string, string, int>> legs, double? price = null)
        {
            var data = new Dictionary<string, string>
            {
                { "class", "combo" },
                { "symbol", symbol },
                { "type", type },
                { "duration", duration },
                { "price", price.ToString() },
            };

            int index = 0;

            foreach (var leg in legs)
            {
                data.Add($"option_symbol[{index}]", leg.Item1);
                data.Add($"side[{index}]", leg.Item2);
                data.Add($"quantity[{index}]", leg.Item3.ToString());

                index++;
            }

            var response = await _requests.PostRequest($"accounts/{accountNumber}/orders", data);
            return JsonConvert.DeserializeObject<OrderResponseRootobject>(response).OrderReponse;
        }

        public async Task<OrderReponse> PlaceOtoOrder(string accountNumber, string duration, List<Tuple<string, int, string, string, string, double?, double?>> legs)
        {
            var data = new Dictionary<string, string>
            {
                { "class", "oto" },
                { "duration", duration },
            };

            int index = 0;

            foreach (var leg in legs)
            {
                data.Add($"symbol[{index}]", leg.Item1);
                data.Add($"quantity[{index}]", leg.Item2.ToString());
                data.Add($"type[{index}]", leg.Item3);
                data.Add($"option_symbol[{index}]", leg.Item4);
                data.Add($"side[{index}]", leg.Item5);
                data.Add($"price[{index}]", leg.Item6.Value == null ? "" : leg.Item6.ToString());
                data.Add($"stop[{index}]", leg.Item7.Value == null ? "" : leg.Item7.ToString());
                
                index++;
            }

            var response = await _requests.PostRequest($"accounts/{accountNumber}/orders", data);
            return JsonConvert.DeserializeObject<OrderResponseRootobject>(response).OrderReponse;
        }

        public async Task<OrderReponse> PlaceOcoOrder(string accountNumber, string duration, List<Tuple<string, int, string, string, string, double?, double?>> legs)
        {
            var data = new Dictionary<string, string>
            {
                { "class", "oco" },
                { "duration", duration },
            };

            int index = 0;

            foreach (var leg in legs)
            {
                data.Add($"symbol[{index}]", leg.Item1);
                data.Add($"quantity[{index}]", leg.Item2.ToString());
                data.Add($"type[{index}]", leg.Item3);
                data.Add($"option_symbol[{index}]", leg.Item4);
                data.Add($"side[{index}]", leg.Item5);
                data.Add($"price[{index}]", leg.Item6.Value == null ? "" : leg.Item6.ToString());
                data.Add($"stop[{index}]", leg.Item7.Value == null ? "" : leg.Item7.ToString());
                
                index++;
            }

            var response = await _requests.PostRequest($"accounts/{accountNumber}/orders", data);
            return JsonConvert.DeserializeObject<OrderResponseRootobject>(response).OrderReponse;
        }

        public async Task<OrderReponse> PlaceOtocoOrder(string accountNumber, string duration, List<Tuple<string, int, string, string, string, double?, double?>> legs)
        {
            var data = new Dictionary<string, string>
            {
                { "class", "otoco" },
                { "duration", duration },
            };

            int index = 0;

            foreach (var leg in legs)
            {
                data.Add($"symbol[{index}]", leg.Item1);
                data.Add($"quantity[{index}]", leg.Item2.ToString());
                data.Add($"type[{index}]", leg.Item3);
                data.Add($"option_symbol[{index}]", leg.Item4);
                data.Add($"side[{index}]", leg.Item5);
                data.Add($"price[{index}]", leg.Item6.Value == null ? "" : leg.Item6.ToString());
                data.Add($"stop[{index}]", leg.Item7.Value == null ? "" : leg.Item7.ToString());
                
                index++;
            }

            var response = await _requests.PostRequest($"accounts/{accountNumber}/orders", data);
            return JsonConvert.DeserializeObject<OrderResponseRootobject>(response).OrderReponse;
        }
    }
}
