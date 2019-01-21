using System;
using System.Collections.Generic;
using System.Text;
using YobitClient.Models;

namespace YobitClient
{
    public static partial class ApiUrls
    {
        public static string Tickers(IEnumerable<CurrencyPair> pairs, bool ignoreInvalid)
        {
            return string.Format("api/3/ticker/{0}?ignore_invalid={1}", string.Join("-", pairs), Convert.ToByte(ignoreInvalid).ToString());
        }

        public static string Trades(IList<CurrencyPair> pairs, int limit)
        {
            return string.Format("api/3/trades/{0}?limit={1}", string.Join("-", pairs), limit);
        }

        public static string Depth(IList<CurrencyPair> pairs)
        {
            return string.Format("api/3/depth/{0}", string.Join("-", pairs));
        }
    }
}
