using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YobitClient.Models;

namespace YobitClient
{
    public partial class YobitClient
    {
        public async Task<Ticker> GetTicker(CurrencyPair pair)
        {
            var result = await GetTickers(new [] {pair});
            return result.FirstOrDefault();
        }


        public async Task<List<Ticker>> GetTickers(IEnumerable<CurrencyPair> pairs, bool ignoreInvalid = false)
        {
            var currencyPairs = pairs as IList<CurrencyPair> ?? pairs.ToList();
            var res = await QueryPublic<Dictionary<string, Ticker>>(ApiUrls.Tickers(currencyPairs, ignoreInvalid));
            return res.Result?.Select(kvp =>
                {
                    kvp.Value.CurrencyPair = currencyPairs.First(p => kvp.Key == p.ToString());
                    return kvp.Value;
                }).ToList();
        }

        public async Task<Dictionary<string, List<Trade>>> GetTrades(CurrencyPair pair, int limit = 100)
        {
            return await GetTrades(new[] { pair });
        }


        public async Task<Dictionary<string, List<Trade>>> GetTrades(IEnumerable<CurrencyPair> pairs, int limit = 100)
        {
            var currencyPairs = pairs as IList<CurrencyPair> ?? pairs.ToList();
            var res = await QueryPublic<Dictionary<string, List<Trade>>>(ApiUrls.Trades(currencyPairs, limit));
            return res.Result;
        }


        public async Task<Dictionary<string, OrderBook>> GetDepth(CurrencyPair pair)
        {
            return await GetDepth(new[] { pair });
        }


        public async Task<Dictionary<string, OrderBook>> GetDepth(IEnumerable<CurrencyPair> pairs)
        {
            var currencyPairs = pairs as IList<CurrencyPair> ?? pairs.ToList();
            var res = await QueryPublic<Dictionary<string, OrderBook>>(ApiUrls.Depth(currencyPairs));
            return res.Result;
        }
    }
}