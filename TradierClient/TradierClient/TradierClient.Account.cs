using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Tradier.Client.Helpers;
using Tradier.Client.Models;

// ReSharper disable once CheckNamespace
namespace Tradier.Client
{
    public sealed partial class TradierClient
    {
        public async Task<Profile> GetUserProfile()
        {
            Requests<ProfileRootObject> request = new Requests<ProfileRootObject>(_httpClient);
            return (await request.GetDeserialized("user/profile")).Profile;
        }

        public async Task<Balances> GetBalances(string accountNumber)
        {
            Requests<BalanceRootObject> request = new Requests<BalanceRootObject>(_httpClient);
            return (await request.GetDeserialized($"accounts/{accountNumber}/balances")).Balances;
        }

        public async Task<Positions> GetPositions(string accountNumber)
        {
            Requests<PositionsRootobject> request = new Requests<PositionsRootobject>(_httpClient);
            return (await request.GetDeserialized($"accounts/{accountNumber}/positions")).Positions;
        }

        public async Task<History> GetHistory(string accountNumber, int page = 1, int limitPerPage = 25)
        {
            Requests<HistoryRootobject> request = new Requests<HistoryRootobject>(_httpClient);
            return (await request.GetDeserialized($"accounts/{accountNumber}/history?page={page}&limit={limitPerPage}")).History;
        }

        public async Task<GainLoss> GetGainLoss(string accountNumber, int page = 1, int limitPerPage = 25)
        {
            Requests<GainLossRootobject> request = new Requests<GainLossRootobject>(_httpClient);
            return (await request.GetDeserialized($"accounts/{accountNumber}/gainloss?page={page}&limit={limitPerPage}")).GainLoss;
        }

        public async Task<Orders> GetOrders(string accountNumber)
        {
            Requests<OrdersRootobject> request = new Requests<OrdersRootobject>(_httpClient);
            return (await request.GetDeserialized($"accounts/{accountNumber}/orders")).Orders;
        }

        public async Task<Order> GetOrder(string accountNumber, string orderId)
        {
            Requests<Orders> request = new Requests<Orders>(_httpClient);
            return (await request.GetDeserialized($"accounts/{accountNumber}/orders/{orderId}")).Order.FirstOrDefault();
        }
    }
}