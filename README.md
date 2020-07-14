[![NuGet](https://img.shields.io/nuget/v/tradier-dotnet-client.svg)](https://www.nuget.org/packages/tradier-dotnet-client/) ![Tradier .NET Client - Deploy](https://github.com/vitali-karmanov/tradier-dotnet-client/workflows/Tradier%20.NET%20Client%20-%20Deploy/badge.svg) [![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)



# Tradier .NET Client

Tradier .NET Client is a .NET Library for the [Tradier API](https://documentation.tradier.com/) to interact with the Tradier Brokerage platform to get account information, market data, place orders, and create watchlists. 

In order to use this client you will need to have an Access Token from Tradier for either the [Developer Sandbox](https://developer.tradier.com/user/sign_up) or the [Brokerage Account](https://documentation.tradier.com/brokerage-api).

## Include the Client

To implement the Library into your project, install [NuGet package](https://www.nuget.org/packages/tradier-dotnet-client/) into your solution/project by running it in the Package Manager Console.
````
PM> Install-Package TradierDotNetClient
````
Or by searching TradierDotNetClient in the Package Manager searching bar.

To implement the client into your project, include the `Tradier.Client` namespace:
```csharp
using Tradier.Client;
```

To instantiate a new client in your program, include the following:

```csharp
TradierClient client = new TradierClient("<TOKEN>");
```

You can also instantiate a new client using an existing HttpClient that you previously created. To the following:
```csharp
TradierClient client = new TradierClient(myHttpClient, "<TOKEN>");
```

Also, you can instantiate a new client with a default account number. To do so include the following:

```csharp
TradierClient client = new TradierClient("<TOKEN>", "<ACCOUNT_NUMBER>");
```
**On an important note**, the client's constructor default setting is to use the Developer Sandbox. To set the client to Brokerage Account, you need to explicitly set it to Production by including the property `useProduction` and set it to true:

```csharp
// Constructor with token and using production endpoints
TradierClient client = new TradierClient("<TOKEN>", useProduction: true);

// Constructor with token, default account number, and using production endpoints
TradierClient clientWithDefaultAcc = new TradierClient("<TOKEN>", "<ACCOUNT_NUMBER>", useProduction: true);
```

<br/>
<div align="right">
    <b><a href="#tradier-net-client">↥ back to top</a></b>
</div>
<br/>

## Use the Client

The client is separated into different sections and follows the [API documentation](https://documentation.tradier.com/) outline. Below are described all the supported API calls for each section. For more information refer to [Wiki](https://github.com/vitali-karmanov/tradier-dotnet-client/wiki).

### [Account](https://github.com/vitali-karmanov/tradier-dotnet-client/wiki/Using-Account-methods)
```csharp
Profile userProfile = await client.Account.GetUserProfile();
Balances balance = await client.Account.GetBalances();
Positions positions = await client.Account.GetPositions();
History history = await client.Account.GetHistory();
GainLoss gainLoss = await client.Account.GetGainLoss();
Orders orders = await client.Account.GetOrders();
Order getOrder = await client.Account.GetOrder(orders.Order.FirstOrDefault().Id);
```

### [Authentication](https://github.com/vitali-karmanov/tradier-dotnet-client/wiki/Using-Authentication-methods)
```csharp
Token authentication = await client.Authentication.CreateAccessToken("<CODE>");
Token authentication = await client.Authentication.RefreshAccessToken("<TOKEN>");
```

### [Market Data](https://github.com/vitali-karmanov/tradier-dotnet-client/wiki/Using-Market-Data-methods)
```csharp
Quotes quotes = await client.MarketData.GetQuotes("AAPL, NFLX");
Quotes quotes1 = await client.MarketData.PostGetQuotes("AAPL, NFLX");
Options options = await client.MarketData.GetOptionChain("AAPL", "August 21, 2020");
Strikes strikes = await client.MarketData.GetStrikes("UNG", "August 21, 2020");
Expirations expirations = await client.MarketData.GetOptionExpirations("AAPL");
List<Symbol> lookup = await client.MarketData.LookupOptionSymbols("SPY");
HistoricalQuotes historicalQuotes = await client.MarketData.GetHistoricalQuotes("AAPL", "daily", "January 1, 2020", "May 15, 2020");
Series timeSales = await client.MarketData.GetTimeSales("AAPL", "1min", "July 1, 2020", "July 11, 2020");
Securities securities = await client.MarketData.GetEtbSecurities();
Clock clock = await client.MarketData.GetClock();
Calendar calendar = await client.MarketData.GetCalendar();
Securities securitiesFilter = await client.MarketData.SearchCompanies("NY");
Securities lookup1 = await client.MarketData.LookupSymbol("goog");
```
### [Trading](https://github.com/vitali-karmanov/tradier-dotnet-client/wiki/Using-Trading-methods)

```csharp
OrderPreviewResponse order = (OrderPreviewResponse) await client.Trading.PlaceEquityOrder("WMT", "buy", 10, "limit", "day", 1.00, preview: true);
OrderReponse order1 = (OrderReponse) await client.Trading.PlaceOptionOrder("WMT", "WMT200717C00129000", "buy_to_open", 1, "limit", "day", 10.00);
OrderReponse order2 = (OrderReponse) await client.Trading.PlaceMultilegOrder("WMT", "debit", "day", new List<(string, string, int)> { ("WMT200717C00129000", "buy_to_open", 1), ("WMT200717C00132000", "sell_to_open", 1) }, 1.30);
OrderReponse order3 = (OrderReponse) await client.Trading.PlaceComboOrder("SPY", "limit", "day", new List<(string, string, int)> { ("SPY", "buy", 1), ("SPY140118C00195000", "buy_to_open", 1) }, 1.00);
OrderReponse order4 = (OrderReponse) await client.Trading.PlaceOtoOrder("day", new List<(string, int, string, string, string, double?, double?)> { ("WMT", 1, "limit", "WMT200717C00129000", "buy_to_open", 1.00, null), ("WMT", 1, "limit", "WMT200717C00129000", "sell_to_close", 1.10, null) });
OrderReponse order5 = (OrderReponse) await client.Trading.PlaceOcoOrder("day", new List<(string, int, string, string, string, double?, double?)> { ("SPY", 1, "limit", "SPY140118C00195000", "buy_to_open", 1.00, null), ("SPY", 1, "limit", "SPY140118C00195000", "sell_to_close", 1.10, null) });
OrderReponse order6 = await client.Trading.ModifyOrder(order1.Id, "limit", "day", 5.00);
order1 = await client.Trading.CancelOrder(order1.Id);
```

### [Watchlist](https://github.com/vitali-karmanov/tradier-dotnet-client/wiki/Using-Watchlist-methods)

```csharp
Watchlists watchlists = await client.Watchlist.GetWatchlists();
Watchlist newWatchlist = await client.Watchlist.CreateWatchlist("My Watchlist", "AAPL,IBM");
Watchlist dafaultWatchlist = await client.Watchlist.GetWatchlist(newWatchlist.Id);
Watchlist updatedWatchlist = await client.Watchlist.UpdateWatchlist(newWatchlist.Id, "My First Watchlist", "SPY");
Watchlist addSymbolWatchlist = await client.Watchlist.AddSymbolsToWatchlist(newWatchlist.Id, "NFLX");
Watchlist removeSymbolFromWatchlist = await client.Watchlist.RemoveSymbolFromWatchlist(newWatchlist.Id, "NFLX");
Watchlists deleteWatclist = await client.Watchlist.DeleteWatchlist(newWatchlist.Id);
```

<br/>
<div align="right">
    <b><a href="#tradier-net-client">↥ back to top</a></b>
</div>
<br/>

## Authors

* **[Henrique Tedeschi](https://github.com/htedeschi)**
* **[Vitali Karmanov](https://github.com/vitali-karmanov)**

## Disclaimer

This Wrapper is NOT an official .NET Tradier Library. This library is provided "as is" without expressed or implied warranty of any kind. Please use at your own risk and discretion.

## License
This Library is provided under the Apache 2.0 License - see the [LICENSE](https://github.com/vitali-karmanov/tradier-dotnet-client/blob/master/LICENSE) file for details
