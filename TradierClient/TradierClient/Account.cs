using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using Tradier.Client.Helpers;
using Tradier.Client.Models.Account;

// ReSharper disable once CheckNamespace
namespace Tradier.Client
{
    /// <summary>
    /// The <c>Account</c> class. 
    /// </summary>
    public class Account
    {
        private readonly Requests _requests;

        /// <summary>
        /// Account Constructor
        /// </summary>
        public Account(Requests requests)
        {
            _requests = requests;
        }

        /// <summary>
        /// The user’s profile contains information pertaining to the user and his/her accounts
        /// </summary>
        public async Task<Profile> GetUserProfile()
        {
            var response = await _requests.GetRequest("user/profile");
            return JsonConvert.DeserializeObject<ProfileRootObject>(response).Profile;
        }

        /// <summary>
        /// Get balances information for a specific user account.
        /// </summary>
        public async Task<Balances> GetBalances(string accountNumber)
        {
            var response = await _requests.GetRequest($"accounts/{accountNumber}/balances");
            return JsonConvert.DeserializeObject<BalanceRootObject>(response).Balances;
        }

        /// <summary>
        /// Get the current positions being held in an account. These positions are updated intraday via trading
        /// </summary>
        public async Task<Positions> GetPositions(string accountNumber)
        {
            var response = await _requests.GetRequest($"accounts/{accountNumber}/positions");
            return JsonConvert.DeserializeObject<PositionsRootobject>(response).Positions;
        }

        /// <summary>
        /// Get historical activity for an account
        /// </summary>
        public async Task<History> GetHistory(string accountNumber, int page = 1, int limitPerPage = 25)
        {
            var response = await _requests.GetRequest($"accounts/{accountNumber}/history?page={page}&limit={limitPerPage}");
            return JsonConvert.DeserializeObject<HistoryRootobject>(response).History;
        }

        /// <summary>
        /// Get cost basis information for a specific user account
        /// </summary>
        public async Task<GainLoss> GetGainLoss(string accountNumber, int page = 1, int limitPerPage = 25)
        {
            var response = await _requests.GetRequest($"accounts/{accountNumber}/gainloss?page={page}&limit={limitPerPage}");
            return JsonConvert.DeserializeObject<GainLossRootobject>(response).GainLoss;
        }

        /// <summary>
        /// Retrieve orders placed within an account
        /// </summary>
        public async Task<Orders> GetOrders(string accountNumber)
        {
            var response = await _requests.GetRequest($"accounts/{accountNumber}/orders");
            return JsonConvert.DeserializeObject<OrdersRootobject>(response).Orders;
        }

        /// <summary>
        /// Get detailed information about a previously placed order
        /// </summary>
        public async Task<Order> GetOrder(string accountNumber, int orderId)
        {
            var response = await _requests.GetRequest($"accounts/{accountNumber}/orders/{orderId}");
            return JsonConvert.DeserializeObject<Orders>(response).Order.FirstOrDefault();
        }
    }
}