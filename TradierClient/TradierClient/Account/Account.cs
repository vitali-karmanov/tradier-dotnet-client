using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Tradier.Client.Config;
using Tradier.Client.Helpers;
using Tradier.Client.Models;

// ReSharper disable once CheckNamespace
namespace Tradier.Client
{
    public class Account
    {
        private readonly HttpClient _httpClient;
        private readonly TradierClientConfig _tradierClientConfig;

        public Account(HttpClient httpClient, TradierClientConfig tradierClientConfig)
        {
            _httpClient = httpClient;
            _tradierClientConfig = tradierClientConfig;
        }

        public async Task<Profile> GetUserProfile()
        {
            Requests<ProfileRootObject> request = new Requests<ProfileRootObject>(_httpClient);
            return (await request.GetDeserialized("user/profile")).Profile;
        }

        public async Task<Balances> GetBalances()
        {
            Requests<BalanceRootObject> request = new Requests<BalanceRootObject>(_httpClient);
            return (await request.GetDeserialized($"accounts/{_tradierClientConfig.AccountNumber}/balances")).Balances;
        }

        public async Task<Positions> GetPositions()
        {
            Requests<PositionsRootobject> request = new Requests<PositionsRootobject>(_httpClient);
            return (await request.GetDeserialized($"accounts/{_tradierClientConfig.AccountNumber}/positions")).Positions;
        }

        public async Task<History> GetHistory(int page = 1, int limitPerPage = 25)
        {
            Requests<HistoryRootobject> request = new Requests<HistoryRootobject>(_httpClient);
            return (await request.GetDeserialized($"accounts/{_tradierClientConfig.AccountNumber}/history?page={page}&limit={limitPerPage}")).History;
        }

        public async Task<GainLoss> GetGainLoss(int page = 1, int limitPerPage = 25)
        {
            Requests<GainLossRootobject> request = new Requests<GainLossRootobject>(_httpClient);
            return (await request.GetDeserialized($"accounts/{_tradierClientConfig.AccountNumber}/gainloss?page={page}&limit={limitPerPage}")).GainLoss;
        }

        public async Task<Orders> GetOrders()
        {
            Requests<OrdersRootobject> request = new Requests<OrdersRootobject>(_httpClient);
            return (await request.GetDeserialized($"accounts/{_tradierClientConfig.AccountNumber}/orders")).Orders;
        }

        public async Task<Order> GetOrder(string orderId)
        {
            Requests<Orders> request = new Requests<Orders>(_httpClient);
            return (await request.GetDeserialized($"accounts/{_tradierClientConfig.AccountNumber}/orders/{orderId}")).Order.FirstOrDefault();
        }
    }
}