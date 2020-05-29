
# Tradier .NET Client

Tradier .NET Client is a .NET Library for the [Tradier API](https://documentation.tradier.com/) to interact with the Tradier Brokerage platform to get account information, market data, place orders, and create watchlists. 

In order to use this client you will need to have an Access Token from Tradier for either the [Developer Sandbox](https://developer.tradier.com/user/sign_up) or the [Brokerage Account](https://documentation.tradier.com/brokerage-api).

## Usage

To implement the Library into your project, install [NuGet package]() into your solution/project by running in the Package Manager Console.
````
PM> Install-Package TradierDotNetClient
````
Or by searching TradierDotNetClient in the Package Manager searching bar.

### Create the Client
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

### Use the Client

The client is separeted into different sections and follows the [API documentation](https://documentation.tradier.com/) outline. Below are described all the supported API calls for each section. For more information refere to [Wiki](https://github.com/vitali-karmanov/tradier-dotnet-client/wiki).

#### Account
```csharp
Profile userProfile = await client.Account.GetUserProfile();
Balances balance = await client.Account.GetBalances(accountNumber);
Positions positions = await client.Account.GetPositions(accountNumber);
History history = await client.Account.GetHistory(accountNumber);
GainLoss gainLoss = await client.Account.GetGainLoss(accountNumber);
Orders orders = await client.Account.GetOrders(accountNumber);
Order order = await client.Account.GetOrder(accountNumber, orderId);
```

#### Market Data
```csharp
Quotes quotes = await client.MarketData.GetQuotes("AAPL, NFLX");
Quotes quotes = await client.MarketData.PostGetQuotes("AAPL, NFLX");
Options options = await client.MarketData.GetOptionChain("AAPL", "2020-05-27");
Expirations expirations = await client.MarketData.GetOptionExpirations("AAPL");
Strikes strikes = await client.MarketData.GetStrikes("UNG", "May 15, 2020");
HistoricalQuotes historicalQuotes = 
	await client.MarketData.GetHistoricalQuotes("AAPL", "daily", "January 1, 2020", "May 15, 2020");
Series timeSales = 
	await client.MarketData.GetTimeSales("AAPL", "1min", "May 1, 2020", "May 15, 2020");
Securities securities = await client.MarketData.GetEtbSecurities();
Clock clock = await client.MarketData.GetClock();
Calendar calendar = await client.MarketData.GetCalendar();
Securities securitiesFilter = await client.MarketData.SearchCompanies("NY");
Securities lookup = await client.MarketData.LookupSymbol("goog");

```
### Trading
Place equity and complex option trades including advanced orders.
For all the trading related usages, make sure you include the Trading Model namespace:
```csharp
using Tradier.Client.Models.Trading;
```

#### Place Equity Order
Place an order to trade an equity security.
```csharp
OrderReponse order = 
	await client.Trading.PlaceEquityOrder(accountNumber, "equity", "AAPL", "buy", "10", "market", "day", "1.00", "1.00");
```

#### Place Option Order
Place an order to trade a single option.
```csharp
IOrder order = 
	await client.Trading.PlaceOptionOrder(accountNumber, "option", "SPY", "SPY140118C00195000", "buy_to_open", "10", "market", "day", "1.00", "1.00", preview: true);
```

#### Place Multileg Order
Place a multileg order with up to 4 legs.
```csharp
List<string> optionSymbols = new List<string>();
optionSymbols.Add("SPY190605C00282000");
optionSymbols.Add("SPY190605C00286000");

List<string> sides = new List<string>();
sides.Add("buy_to_open");
sides.Add("buy_to_close");

List<string> quantities = new List<string>();
sides.Add("10");
sides.Add("10");

OrderReponse order = 
	await client.Trading.PlaceMultilegOrder(accountNumber, "option", "AAPL", "market", "day", "1.00", optionSymbols, sides, quantities);
```

#### Modify Order
Modify an order. You may change some or all of these parameters. You may not change the session of a pre/post market session order. Send only the parameters you would like to adjust.
```csharp
OrderReponse order = 
	await client.Trading.ModifyOrder(accountNumber, orderId, "limit", "day", "1.00", "1.00");
```

#### Cancel Order
Cancel an order.
```csharp
OrderReponse order = await client.Trading.CancelOrder(accountNumber, orderId);
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
This Library is provided under the [MIT License](https://raw.github.com/sta/websocket-sharp/master/LICENSE.txt).
