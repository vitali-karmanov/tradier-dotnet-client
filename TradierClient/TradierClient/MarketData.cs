using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Tradier.Client.Helpers;
using Tradier.Client.Models.MarketData;

// ReSharper disable once CheckNamespace
namespace Tradier.Client
{
    /// <summary>
    /// The <c>MarketData</c> class
    /// </summary>
    public class MarketData
    {
        private readonly Requests _requests;

        /// <summary>
        /// The MarketData constructor
        /// </summary>
        public MarketData(Requests requests)
        {
            _requests = requests;
        }

        /// <summary>
        /// Get all quotes in an option chain
        /// </summary>
        public async Task<Options> GetOptionChain(string symbol, DateTime expiration, bool greeks = false)
        {
            string stringExpiration = expiration.ToString("yyyy-MM-dd");
            return await GetOptionChain(symbol, stringExpiration, greeks);
        }

        /// <summary>
        /// Get all quotes in an option chain
        /// </summary>
        public async Task<Options> GetOptionChain(string symbol, string expiration, bool greeks = false)
        {
            var response = await _requests.GetRequest($"markets/options/chains?symbol={symbol}&expiration={expiration}&greeks={greeks}");
            return JsonConvert.DeserializeObject<OptionChainRootobject>(response).Options;
        }

        /// <summary>
        /// Get expiration dates for a particular underlying
        /// </summary>
        public async Task<Expirations> GetOptionExpirations(string symbol, bool? includeAllRoots = false, bool? strikes = false)
        {
            var response = await _requests.GetRequest($"markets/options/expirations?symbol={symbol}&includeAllRoots={includeAllRoots}&strikes={strikes}");
            return JsonConvert.DeserializeObject<OptionExpirationsRootobject>(response).Expirations;
        }

        /// <summary>
        /// Get a list of symbols using a keyword lookup on the symbols description
        /// </summary>
        public async Task<Quotes> GetQuotes(string symbols, bool greeks = false)
        {
            List<string> listSymbols = symbols.Split(',').Select(x => x.Trim()).ToList();
            return await GetQuotes(listSymbols, greeks);
        }

        /// <summary>
        /// Get a list of symbols using a keyword lookup on the symbols description
        /// </summary>
        public async Task<Quotes> GetQuotes(List<string> symbols, bool greeks = false)
        {
            string strSymbols = String.Join(",", symbols).Trim();

            var response = await _requests.GetRequest($"markets/quotes?symbols={strSymbols}&greeks={greeks}");
            return JsonConvert.DeserializeObject<QuoteRootobject>(response).Quotes;
        }

        /// <summary>
        /// Get historical pricing for a security
        /// </summary>
        public async Task<HistoricalQuotes> GetHistoricalQuotes(string symbol, string interval, string start, string end, CultureInfo culture = null)
        {
            culture ??= new CultureInfo("en-US");
            DateTime startDateTime = DateTime.Parse(start, culture);
            DateTime endDateTime = DateTime.Parse(end, culture);

            return await GetHistoricalQuotes(symbol, interval, startDateTime, endDateTime);
        }

        /// <summary>
        /// Get historical pricing for a security
        /// </summary>
        public async Task<HistoricalQuotes> GetHistoricalQuotes(string symbol, string interval, DateTime start, DateTime end)
        {

            string stringStart = start.ToString("yyyy-MM-dd");
            string stringEnd = end.ToString("yyyy-MM-dd");

            var response = await _requests.GetRequest($"markets/history?symbol={symbol}&interval={interval}&start={stringStart}&end={stringEnd}");
            return JsonConvert.DeserializeObject<HistoricalQuotesRootobject>(response).History;
        }

        /// <summary>
        /// Get a list of symbols using a keyword lookup on the symbols description using POST request
        /// </summary>
        public async Task<Quotes> PostGetQuotes(string symbols, bool greeks = false)
        {
            List<string> listSymbols = symbols.Split(',').Select(x => x.Trim()).ToList();
            return await PostGetQuotes(listSymbols, greeks);
        }

        /// <summary>
        /// Get a list of symbols using a keyword lookup on the symbols description using POST request
        /// </summary>
        public async Task<Quotes> PostGetQuotes(List<string> symbols, bool greeks = false)
        {
            string strSymbols = String.Join(",", symbols);
            var data = new Dictionary<string, string>
            {
                { "symbols", strSymbols },
                { "greeks", greeks.ToString() },
            };

            var response = await _requests.PostRequest($"markets/quotes", data);
            return JsonConvert.DeserializeObject<QuoteRootobject>(response).Quotes;
        }

        /// <summary>
        /// Get an options strike prices for a specified expiration date
        /// </summary>
        public async Task<Strikes> GetStrikes(string symbol, string expiration, CultureInfo culture = null)
        {
            culture ??= new CultureInfo("en-US");
            DateTime expirationDateTime = DateTime.Parse(expiration, culture);
            return await GetStrikes(symbol, expirationDateTime);
        }

        /// <summary>
        /// Get an options strike prices for a specified expiration date
        /// </summary>
        public async Task<Strikes> GetStrikes(string symbol, DateTime expiration)
        {
            string stringExpiration = expiration.ToString("yyyy-MM-dd");
            var response = await _requests.GetRequest($"markets/options/strikes?symbol={symbol}&expiration={stringExpiration}");
            return JsonConvert.DeserializeObject<OptionStrikesRootobject>(response).Strikes;
        }

        /// <summary>
        /// Time and Sales (timesales) is typically used for charting purposes. It captures pricing across a time slice at predefined intervals.
        /// </summary>
        public async Task<Series> GetTimeSales(string symbol, string interval, string start, string end, string filter = "all", CultureInfo culture = null)
        {
            culture ??= new CultureInfo("en-US");
            DateTime startDateTime = DateTime.Parse(start, culture);
            DateTime endDateTime = DateTime.Parse(end, culture);

            return await GetTimeSales(symbol, interval, startDateTime, endDateTime, filter);
        }

        /// <summary>
        /// Time and Sales (timesales) is typically used for charting purposes. It captures pricing across a time slice at predefined intervals.
        /// </summary>
        public async Task<Series> GetTimeSales(string symbol, string interval, DateTime start, DateTime end, string filter = "all")
        {
            string stringStart = start.ToString("yyyy-MM-dd HH:mm");
            string stringEnd = end.ToString("yyyy-MM-dd HH:mm");

            var response = await _requests.GetRequest($"markets/timesales?symbol={symbol}&interval={interval}&start={stringStart}&end={stringEnd}&session_filter={filter}");
            return JsonConvert.DeserializeObject<TimesalesRootobject>(response).Series;
        }

        /// <summary>
        /// The ETB list contains securities that are able to be sold short with a Tradier Brokerage account.
        /// </summary>
        public async Task<Securities> GetEtbSecurities()
        {
            var response = await _requests.GetRequest($"markets/etb");
            return JsonConvert.DeserializeObject<SecuritiesRootobject>(response).Securities;
        }

        /// <summary>
        /// The ETB list contains securities that are able to be sold short with a Tradier Brokerage account.
        /// </summary>
        public async Task<Clock> GetClock()
        {
            var response = await _requests.GetRequest($"markets/clock");
            return JsonConvert.DeserializeObject<ClockRootobject>(response).Clock;
        }

        /// <summary>
        /// Get the market calendar for the current or given month
        /// </summary>
        public async Task<Models.MarketData.Calendar> GetCalendar(int? month = null, int? year = null)
        {
            var response = await _requests.GetRequest($"markets/calendar?month={month}&year={year}");
            return JsonConvert.DeserializeObject<CalendarRootobject>(response).Calendar;
        }

        /// <summary>
        /// Get the market calendar for the current or given month
        /// </summary>
        public async Task<Securities> SearchCompanies(string query, bool indexes = false)
        {
            var response = await _requests.GetRequest($"markets/search?q={query}&indexes={indexes}");
            return JsonConvert.DeserializeObject<SecuritiesRootobject>(response).Securities;
        }

        /// <summary>
        /// Search for a symbol using the ticker symbol or partial symbol
        /// </summary>
        public async Task<Securities> LookupSymbol(string query, string? exchanges = null, string? types = null)
        {
            var response = await _requests.GetRequest($"lookup?q={query}&exchanges={exchanges}&types={types}");
            return JsonConvert.DeserializeObject<SecuritiesRootobject>(response).Securities;
        }

        /// <summary>
        /// Get all options symbols for the given underlying
        /// </summary>
        public async Task<List<Symbol>> LookupOptionSymbols(string symbol)
        {
            var response = await _requests.GetRequest($"markets/options/lookup?underlying={symbol}");
            return JsonConvert.DeserializeObject<OptionSymbolsRootobject>(response).Symbols;
        }
    }
}