using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tradier.Client.Exceptions;
using Tradier.Client.Helpers;
using Tradier.Client.Models.Trading;

// ReSharper disable once CheckNamespace
namespace Tradier.Client
{
    /// <summary>
    /// The <c>Trading</c> class
    /// </summary>
    public class Trading
    {
        private readonly Requests _requests;
        private readonly string _defaultAccountNumber;

        /// <summary>
        /// The Trading constructor
        /// </summary>
        public Trading(Requests requests, string defaultAccountNumber)
        {
            _requests = requests;
            _defaultAccountNumber = defaultAccountNumber;
        }

        /// <summary>
        /// Place an order using the default account number to trade a single option 
        /// </summary>
        public async Task<IOrder> PlaceOptionOrder(string symbol, string optionSymbol, string side, int quantity, string type, string duration, double? price = null, double? stop = null, bool preview = false)
        {
            if (string.IsNullOrEmpty(_defaultAccountNumber))
            {
                throw new MissingAccountNumberException("The default account number was not defined.");
            }

            return await PlaceOptionOrder(_defaultAccountNumber, symbol, optionSymbol, side, quantity, type, duration, price, stop, preview);
        }

        /// <summary>
        /// Place an order to trade a single option
        /// </summary>
        public async Task<IOrder> PlaceOptionOrder(string accountNumber, string symbol, string optionSymbol, string side, int quantity, string type, string duration, double? price = null, double? stop = null, bool preview = false)
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
                { "preview", preview.ToString() }
            };

            var response = await _requests.PostRequest($"accounts/{accountNumber}/orders", data);

            return GetOrderReponseOrOrderPreviewResponse(response, preview);
        }

        /// <summary>
        /// Place a multileg order using the default account number with up to 4 legs 
        /// </summary>
        public async Task<IOrder> PlaceMultilegOrder(string symbol, string type, string duration, List<(string, string, int)> legs, double? price = null, bool preview = false)
        {
            if (string.IsNullOrEmpty(_defaultAccountNumber))
            {
                throw new MissingAccountNumberException("The default account number was not defined.");
            }

            return await PlaceMultilegOrder(_defaultAccountNumber, symbol, type, duration, legs, price, preview);
        }

        /// <summary>
        /// Place a multileg order with up to 4 legs
        /// </summary>
        public async Task<IOrder> PlaceMultilegOrder(string accountNumber, string symbol, string type, string duration, List<(string, string, int)> legs, double? price = null, bool preview = false)
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

            return GetOrderReponseOrOrderPreviewResponse(response, preview);
        }

        /// <summary>
        /// Place an order using the default account to trade an equity security
        /// </summary>
        public async Task<IOrder> PlaceEquityOrder(string symbol, string side, int quantity, string type, string duration, double? price = null, double? stop = null, bool preview = false)
        {
            if (string.IsNullOrEmpty(_defaultAccountNumber))
            {
                throw new MissingAccountNumberException("The default account number was not defined.");
            }

            return await PlaceEquityOrder(_defaultAccountNumber, symbol, side, quantity, type, duration, price, stop, preview);
        }

        /// <summary>
        /// Place an order to trade an equity security
        /// </summary>
        public async Task<IOrder> PlaceEquityOrder(string accountNumber, string symbol, string side, int quantity, string type, string duration, double? price = null, double? stop = null, bool preview = false)
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
                { "preview", preview.ToString() }
            };

            var response = await _requests.PostRequest($"accounts/{accountNumber}/orders", data);

            return GetOrderReponseOrOrderPreviewResponse(response, preview);
        }

        /// <summary>
        /// Place a combo order using the default account number. This is a specialized type of order consisting of one equity leg and one option leg
        /// </summary>
        public async Task<IOrder> PlaceComboOrder(string symbol, string type, string duration, List<(string, string, int)> legs, double? price = null, bool preview = false)
        {
            if (string.IsNullOrEmpty(_defaultAccountNumber))
            {
                throw new MissingAccountNumberException("The default account number was not defined.");
            }

            return await PlaceComboOrder(_defaultAccountNumber, symbol, type, duration, legs, price, preview);
        }

        /// <summary>
        /// Place a combo order. This is a specialized type of order consisting of one equity leg and one option leg
        /// </summary>
        public async Task<IOrder> PlaceComboOrder(string accountNumber, string symbol, string type, string duration, List<(string, string, int)> legs, double? price = null, bool preview = false)
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

            return GetOrderReponseOrOrderPreviewResponse(response, preview);
        }

        /// <summary>
        /// Place a one-triggers-other order using the default account number. This order type is composed of two separate orders sent simultaneously
        /// </summary>
        public async Task<IOrder> PlaceOtoOrder(string duration, List<(string, int, string, string, string, double?, double?)> legs, bool preview = false)
        {
            if (string.IsNullOrEmpty(_defaultAccountNumber))
            {
                throw new MissingAccountNumberException("The default account number was not defined.");
            }

            return await PlaceOtoOrder(_defaultAccountNumber, duration, legs, preview);
        }

        /// <summary>
        /// Place a one-triggers-other order. This order type is composed of two separate orders sent simultaneously
        /// </summary>
        public async Task<IOrder> PlaceOtoOrder(string accountNumber, string duration, List<(string, int, string, string, string, double?, double?)> legs, bool preview = false)
        {
            var data = new Dictionary<string, string>
            {
                { "class", "oto" },
                { "duration", duration },
                { "preview", preview.ToString() }
            };

            int index = 0;

            foreach (var leg in legs)
            {
                data.Add($"symbol[{index}]", leg.Item1);
                data.Add($"quantity[{index}]", leg.Item2.ToString());
                data.Add($"type[{index}]", leg.Item3);
                data.Add($"option_symbol[{index}]", leg.Item4);
                data.Add($"side[{index}]", leg.Item5);
                data.Add($"price[{index}]", leg.Item6.HasValue ? leg.Item6.ToString() : "");
                data.Add($"stop[{index}]", leg.Item7.HasValue ? leg.Item7.ToString() : "");

                index++;
            }

            var response = await _requests.PostRequest($"accounts/{accountNumber}/orders", data);

            return GetOrderReponseOrOrderPreviewResponse(response, preview);
        }

        /// <summary>
        /// Place a one-cancels-other order using the default account number. This order type is composed of two separate orders sent simultaneously
        /// </summary>
        public async Task<IOrder> PlaceOcoOrder(string duration, List<(string, int, string, string, string, double?, double?)> legs, bool preview = false)
        {
            if (string.IsNullOrEmpty(_defaultAccountNumber))
            {
                throw new MissingAccountNumberException("The default account number was not defined.");
            }

            return await PlaceOcoOrder(_defaultAccountNumber, duration, legs, preview);
        }

        /// <summary>
        /// Place a one-cancels-other order. This order type is composed of two separate orders sent simultaneously
        /// </summary>
        public async Task<IOrder> PlaceOcoOrder(string accountNumber, string duration, List<(string, int, string, string, string, double?, double?)> legs, bool preview = false)
        {
            var data = new Dictionary<string, string>
            {
                { "class", "oco" },
                { "duration", duration },
                { "preview", preview.ToString() }
            };

            int index = 0;

            foreach (var leg in legs)
            {
                data.Add($"symbol[{index}]", leg.Item1);
                data.Add($"quantity[{index}]", leg.Item2.ToString());
                data.Add($"type[{index}]", leg.Item3);
                data.Add($"option_symbol[{index}]", leg.Item4);
                data.Add($"side[{index}]", leg.Item5);
                data.Add($"price[{index}]", leg.Item6.HasValue ? leg.Item6.ToString() : "");
                data.Add($"stop[{index}]", leg.Item7.HasValue ? leg.Item7.ToString() : "");

                index++;
            }

            var response = await _requests.PostRequest($"accounts/{accountNumber}/orders", data);

            return GetOrderReponseOrOrderPreviewResponse(response, preview);
        }

        /// <summary>
        /// Place a one-triggers-one-cancels-other order using the default account number. This order type is composed of three separate orders sent simultaneously
        /// </summary>
        public async Task<IOrder> PlaceOtocoOrder(string duration, List<(string, int, string, string, string, double?, double?)> legs, bool preview = false)
        {
            if (string.IsNullOrEmpty(_defaultAccountNumber))
            {
                throw new MissingAccountNumberException("The default account number was not defined.");
            }

            return await PlaceOtocoOrder(_defaultAccountNumber, duration, legs, preview);
        }

        /// <summary>
        /// Place a one-triggers-one-cancels-other order. This order type is composed of three separate orders sent simultaneously
        /// </summary>
        public async Task<IOrder> PlaceOtocoOrder(string accountNumber, string duration, List<(string, int, string, string, string, double?, double?)> legs, bool preview = false)
        {
            var data = new Dictionary<string, string>
            {
                { "class", "otoco" },
                { "duration", duration },
                { "preview", preview.ToString() }
            };

            int index = 0;

            foreach (var leg in legs)
            {
                data.Add($"symbol[{index}]", leg.Item1);
                data.Add($"quantity[{index}]", leg.Item2.ToString());
                data.Add($"type[{index}]", leg.Item3);
                data.Add($"option_symbol[{index}]", leg.Item4);
                data.Add($"side[{index}]", leg.Item5);
                data.Add($"price[{index}]", leg.Item6.HasValue ? leg.Item6.ToString() : "");
                data.Add($"stop[{index}]", leg.Item7.HasValue ? leg.Item7.ToString() : "");

                index++;
            }

            var response = await _requests.PostRequest($"accounts/{accountNumber}/orders", data);

            return GetOrderReponseOrOrderPreviewResponse(response, preview);
        }

        /// <summary>
        /// Modify an order using the default account number. You may change some or all of these parameters.
        /// </summary>
        public async Task<OrderReponse> ModifyOrder(int orderId, string type = null, string duration = null, double? price = null, double? stop = null)
        {
            if (string.IsNullOrEmpty(_defaultAccountNumber))
            {
                throw new MissingAccountNumberException("The default account number was not defined.");
            }

            return await ModifyOrder(_defaultAccountNumber, orderId, type, duration, price, stop);
        }

        /// <summary>
        /// Modify an order. You may change some or all of these parameters.
        /// </summary>
        public async Task<OrderReponse> ModifyOrder(string accountNumber, int orderId, string type = null, string duration = null, double? price = null, double? stop = null)
        {
            var data = new Dictionary<string, string>
            {
                { "type", type },
                { "duration", duration },
                { "price", price.HasValue ? price.ToString() : "" },
                { "stop", stop.HasValue ? stop.ToString() : "" },
            };

            var response = await _requests.PutRequest($"accounts/{accountNumber}/orders/{orderId}", data);
            return JsonConvert.DeserializeObject<OrderResponseRootobject>(response).OrderReponse;
        }

        /// <summary>
        /// Cancel an order using the default account number
        /// </summary>
        public async Task<OrderReponse> CancelOrder(int orderId)
        {
            if (string.IsNullOrEmpty(_defaultAccountNumber))
            {
                throw new MissingAccountNumberException("The default account number was not defined.");
            }

            return await CancelOrder(_defaultAccountNumber, orderId);
        }

        /// <summary>
        /// Cancel an order using the default account number
        /// </summary>
        public async Task<OrderReponse> CancelOrder(string accountNumber, int orderId)
        {
            var response = await _requests.DeleteRequest($"accounts/{accountNumber}/orders/{orderId}");
            return JsonConvert.DeserializeObject<OrderResponseRootobject>(response).OrderReponse;
        }

        private IOrder GetOrderReponseOrOrderPreviewResponse(string response, bool preview)
        {
            if (preview)
            {
                return JsonConvert.DeserializeObject<OrderPreviewResponseRootobject>(response).OrderPreviewResponse;
            }
            else
            {
                return JsonConvert.DeserializeObject<OrderResponseRootobject>(response).OrderReponse;
            }
        }
    }
}
