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
    public class MarketData
    {
        private readonly Requests _requests;

        public MarketData(Requests requests)
        {
            _requests = requests;
        }

        public async Task<Options> GetOptionChain(string symbol, string expiration, bool greeks = false)
        {
            var response = await _requests.GetRequest($"markets/options/chains?symbol={symbol}&expiration={expiration}&greeks={greeks}");
            return JsonConvert.DeserializeObject<OptionChainRootobject>(response).Options;
        }

        public async Task<Expirations> GetOptionExpirations(string symbol, bool? includeAllRoots = false, bool? strikes = false)
        {
            var response = await _requests.GetRequest($"markets/options/expirations?symbol={symbol}&includeAllRoots={includeAllRoots}&strikes={strikes}");
            return JsonConvert.DeserializeObject<ExpirationRootobject>(response).Expirations;
        }

        public async Task<Quotes> GetQuotes(string symbols, bool greeks = false)
        {
            List<string> listSymbols = symbols.Split(',').Select(x => x.Trim()).ToList();
            return await GetQuotes(listSymbols, greeks);
        }

        public async Task<Quotes> GetQuotes(List<string> symbols, bool greeks = false)
        {
            string strSymbols = String.Join(",", symbols).Trim();

            var response = await _requests.GetRequest($"markets/quotes?symbols={strSymbols}&greeks={greeks}");
            return JsonConvert.DeserializeObject<QuoteRootobject>(response).Quotes;
        }

        public async Task<HistoricalQuotes> GetHistoricalQuotes(string symbol, string interval, string start, string end, CultureInfo culture = null)
        {
            culture ??= new CultureInfo("en-US");
            DateTime startDateTime = DateTime.Parse(start, culture);
            DateTime endDateTime = DateTime.Parse(end, culture);

            return await GetHistoricalQuotes(symbol, interval, startDateTime, endDateTime);
        }

        public async Task<HistoricalQuotes> GetHistoricalQuotes(string symbol, string interval, DateTime start, DateTime end)
        {

            string stringStart = start.ToString("yyyy-MM-dd");
            string stringEnd = end.ToString("yyyy-MM-dd");

            var response = await _requests.GetRequest($"markets/history?symbol={symbol}&interval={interval}&start={stringStart}&end={stringEnd}");
            return JsonConvert.DeserializeObject<HistoricalQuotesRootobject>(response).History;
        }

        public async Task<Quotes> PostGetQuotes(string symbols, bool greeks = false)
        {
            List<string> listSymbols = symbols.Split(',').Select(x => x.Trim()).ToList();
            return await PostGetQuotes(listSymbols, greeks);
        }

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

        public async Task<Strikes> GetStrikes(string symbol, string expiration, CultureInfo culture = null)
        {
            culture ??= new CultureInfo("en-US");
            DateTime expirationDateTime = DateTime.Parse(expiration, culture);
            return await GetStrikes(symbol, expirationDateTime);
        }

        public async Task<Strikes> GetStrikes(string symbol, DateTime expiration)
        {
            string stringExpiration = expiration.ToString("yyyy-MM-dd");
            var response = await _requests.GetRequest($"markets/options/strikes?symbol={symbol}&expiration={stringExpiration}");
            return JsonConvert.DeserializeObject<StrikeRootobject>(response).Strikes;
        }

        public async Task<Series> GetTimeSales(string symbol, string interval, string start, string end, string filter = "all", CultureInfo culture = null)
        {
            culture ??= new CultureInfo("en-US");
            DateTime startDateTime = DateTime.Parse(start, culture);
            DateTime endDateTime = DateTime.Parse(end, culture);

            return await GetTimeSales(symbol, interval, startDateTime, endDateTime, filter);
        }

        public async Task<Series> GetTimeSales(string symbol, string interval, DateTime start, DateTime end, string filter = "all")
        {
            string stringStart = start.ToString("yyyy-MM-dd HH:mm");
            string stringEnd = end.ToString("yyyy-MM-dd HH:mm");

            var response = await _requests.GetRequest($"markets/timesales?symbol={symbol}&interval={interval}&start={stringStart}&end={stringEnd}&session_filter={filter}");
            return JsonConvert.DeserializeObject<SeriesRootobject>(response).Series;
        }

        public async Task<Securities> GetEtbSecurities()
        {
            var response = await _requests.GetRequest($"markets/etb");
            return JsonConvert.DeserializeObject<SecuritiesRootobject>(response).Securities;
        }

        public async Task<Clock> GetClock()
        {
            var response = await _requests.GetRequest($"markets/clock");
            return JsonConvert.DeserializeObject<ClockRootobject>(response).Clock;
        }

        public async Task<Models.MarketData.Calendar> GetCalendar(int? month = null, int? year = null)
        {
            var response = await _requests.GetRequest($"markets/calendar?month={month}&year={year}");
            return JsonConvert.DeserializeObject<CalendarRootobject>(response).Calendar;
        }

        public async Task<Securities> SearchCompanies(string query, bool indexes = false)
        {
            var response = await _requests.GetRequest($"markets/search?q={query}&indexes={indexes}");
            return JsonConvert.DeserializeObject<SecuritiesRootobject>(response).Securities;
        }
    }
}