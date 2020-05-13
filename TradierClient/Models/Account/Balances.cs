using Newtonsoft.Json;


namespace Tradier.Client.Models.Account
{
    public class BalanceRootObject
    {
        [JsonProperty("balances")]
        public Balances Balances { get; set; }
    }

    public class Balances
    {
        [JsonProperty("option_short_value")]
        public int OptionShortValue { get; set; }

        [JsonProperty("total_equity")]
        public float TotalEquity { get; set; }

        [JsonProperty("account_number")]
        public string AccountNumber { get; set; }

        [JsonProperty("account_type")]
        public string AccountType { get; set; }

        [JsonProperty("close_pl")]
        public float ClosePL { get; set; }

        [JsonProperty("current_requirement")]
        public float CurrentRequirement { get; set; }

        [JsonProperty("equity")]
        public int Equity { get; set; }

        [JsonProperty("long_market_value")]
        public float LongMarketValue { get; set; }

        [JsonProperty("market_value")]
        public float MarketValue { get; set; }

        [JsonProperty("open_pl")]
        public float OpenPL { get; set; }

        [JsonProperty("option_long_value")]
        public float OptionLongValue { get; set; }

        [JsonProperty("option_requirement")]
        public int OptionRequirement { get; set; }

        [JsonProperty("pending_orders_count")]
        public int PendingOrdersCount { get; set; }

        [JsonProperty("short_market_value")]
        public int ShortMarketValue { get; set; }

        [JsonProperty("stock_long_value")]
        public float StockLongValue { get; set; }

        [JsonProperty("total_cash")]
        public float TotalCash { get; set; }

        [JsonProperty("uncleared_funds")]
        public int UnclearedFunds { get; set; }

        [JsonProperty("pending_cash")]
        public float PendingCash { get; set; }

        [JsonProperty("margin")]
        public Margin Margin { get; set; }

        [JsonProperty("cash")]
        public Cash Cash { get; set; }

        [JsonProperty("pdt")]
        public PatternDayTrader PatternDayTrader { get; set; }
    }

    public class Margin
    {
        [JsonProperty("fed_call")]
        public int FedCall { get; set; }

        [JsonProperty("maintenance_call")]
        public int MaintenanceCall { get; set; }

        [JsonProperty("option_buying_power")]
        public float OptionBuyingPower { get; set; }

        [JsonProperty("stock_buying_power")]
        public float StockBuyingPower { get; set; }

        [JsonProperty("stock_short_value")]
        public int StockShortValue { get; set; }

        [JsonProperty("sweep")]
        public int Sweep { get; set; }
    }

    public class Cash
    {
        [JsonProperty("cash_available")]
        public float CashAvailable { get; set; }

        [JsonProperty("sweep")]
        public int Sweep { get; set; }

        [JsonProperty("unsettled_funds")]
        public float UnsettledFunds { get; set; }
    }

    public class PatternDayTrader
    {
        [JsonProperty("fed_call")]
        public int FedCall { get; set; }

        [JsonProperty("maintenance_call")]
        public int MaintenanceCall { get; set; }

        [JsonProperty("option_buying_power")]
        public float OptionBuyingPower { get; set; }

        [JsonProperty("stock_buying_power")]
        public float StockBuyingPower { get; set; }

        [JsonProperty("stock_short_value")]
        public int StockShortValue { get; set; }
    }

}