[![NuGet](https://img.shields.io/nuget/v/tradier-dotnet-client.svg)](https://www.nuget.org/packages/tradier-dotnet-client/)

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
**On an important note**, the client's constructor default setting is to use the Developer Sandbox. To set the client to Brokerage Account, you need to explicitly set it to Production by including the property `useProduction` and set it to true:

```csharp
TradierClient client = new TradierClient("<TOKEN>", useProduction: true);
```

*Note that it is not needed to pass a `HttpClient` to the client. TradierDotNetClient already creates one using the `HttpClientFactory` helping it to keep control over the requests.*

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
Balances balance = await client.Account.GetBalances(accountNumber);
Positions positions = await client.Account.GetPositions(accountNumber);
History history = await client.Account.GetHistory(accountNumber);
GainLoss gainLoss = await client.Account.GetGainLoss(accountNumber);
Orders orders = await client.Account.GetOrders(accountNumber);
Order order = await client.Account.GetOrder(accountNumber, orderId);
```

### [Market Data](https://github.com/vitali-karmanov/tradier-dotnet-client/wiki/Using-Market-Data-methods)
```csharp
Quotes quotes = await client.MarketData.GetQuotes("AAPL, NFLX");
Quotes quotes = await client.MarketData.PostGetQuotes("AAPL, NFLX");
Options options = await client.MarketData.GetOptionChain("AAPL", "2020-05-27");
Expirations expirations = await client.MarketData.GetOptionExpirations("AAPL");
Strikes strikes = await client.MarketData.GetStrikes("UNG", "May 15, 2020");
HistoricalQuotes historicalQuotes = await client.MarketData.GetHistoricalQuotes("AAPL", "daily", "January 1, 2020", "May 15, 2020");
Series timeSales = await client.MarketData.GetTimeSales("AAPL", "1min", "May 1, 2020", "May 15, 2020");
Securities securities = await client.MarketData.GetEtbSecurities();
Clock clock = await client.MarketData.GetClock();
Calendar calendar = await client.MarketData.GetCalendar();
Securities securitiesFilter = await client.MarketData.SearchCompanies("NY");
Securities lookup = await client.MarketData.LookupSymbol("goog");

```
### [Trading](https://github.com/vitali-karmanov/tradier-dotnet-client/wiki/Using-Trading-methods)

```csharp
OrderReponse order = await client.Trading.PlaceEquityOrder(accountNumber, "equity", "AAPL", "buy", "10", "market", "day", "1.00", "1.00");
OrderReponse order = await client.Trading.PlaceOptionOrder(accountNumber, "option", "SPY", "SPY140118C00195000", "buy_to_open", "10", "market", "day", "1.00", "1.00", preview: true);
OrderReponse order = await client.Trading.PlaceMultilegOrder(accountNumber, "multileg", "MFA", "credit", "day", "0.10", new List<string> { "MFA200717C00002000", "MFA200717C00003000"}, new List<string> { "sell_to_open", "buy_to_open" } , new List<string> { "1", "1"});
OrderReponse order = await client.Trading.ModifyOrder(accountNumber, orderId, "limit", "day", "1.00", "1.00");
OrderReponse order = await client.Trading.CancelOrder(accountNumber, orderId);
```

### [Watchlist](https://github.com/vitali-karmanov/tradier-dotnet-client/wiki/Using-Watchlist-methods)

```csharp
Watchlists watchlists = await client.Watchlist.GetWatchlists();
Watchlists dafaultWatchlist = await client.Watchlist.GetWatchlist("<WATCHLIST_ID>");
Watchlists newWatchlist = await client.Watchlist.CreateWatchlist("My Watchlist", "AAPL,IBM");
Watchlists updatedWatchlist = await client.Watchlist.UpdateWatchlist("my-watchlist", "My First Watchlist", "SPY");
await client.Watchlist.AddSymbolsToWatchlist("my-watchlist", "NFLX");
await client.Watchlist.RemoveSymbolFromWatchlist("my-watchlist", "AAPL");
Watchlists deleteWatclist = await client.Watchlist.DeleteWatchlist("my-watchlist");
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
