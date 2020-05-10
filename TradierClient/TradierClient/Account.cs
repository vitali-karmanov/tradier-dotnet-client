using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using Tradier.Client.Helpers;
using Tradier.Client.Models.Account;

// ReSharper disable once CheckNamespace
namespace Tradier.Client
{
    public class Account
    {
        private readonly Requests _requests;

        public Account(Requests requests)
        {
            _requests = requests;
        }

        public async Task<Profile> GetUserProfile()
        {
            var response = await _requests.GetContent("user/profile");
            return JsonConvert.DeserializeObject<ProfileRootObject>(response).Profile;
        }

        public async Task<Balances> GetBalances(string accountNumber)
        {
            var response = await _requests.GetContent($"accounts/{accountNumber}/balances");
            return JsonConvert.DeserializeObject<BalanceRootObject>(response).Balances;
        }

        public async Task<Positions> GetPositions(string accountNumber)
        {
            var response = await _requests.GetContent($"accounts/{accountNumber}/positions");
            return JsonConvert.DeserializeObject<PositionsRootobject>(response).Positions;
        }

        public async Task<History> GetHistory(string accountNumber, int page = 1, int limitPerPage = 25)
        {
            var response = await _requests.GetContent($"accounts/{accountNumber}/history?page={page}&limit={limitPerPage}");
            return JsonConvert.DeserializeObject<HistoryRootobject>(response).History;
        }

        public async Task<GainLoss> GetGainLoss(string accountNumber, int page = 1, int limitPerPage = 25)
        {
            var response = await _requests.GetContent($"accounts/{accountNumber}/gainloss?page={page}&limit={limitPerPage}");
            return JsonConvert.DeserializeObject<GainLossRootobject>(response).GainLoss;
        }

        public async Task<Orders> GetOrders(string accountNumber)
        {
            var response = await _requests.GetContent($"accounts/{accountNumber}/orders");
            return JsonConvert.DeserializeObject<OrdersRootobject>(response).Orders;
        }

        public async Task<Order> GetOrder(string accountNumber, string orderId)
        {
            var response = await _requests.GetContent($"accounts/{accountNumber}/orders/{orderId}");
            return JsonConvert.DeserializeObject<Orders>(response).Order.FirstOrDefault();
        }
    }
}