using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YobitClient.Models;

namespace YobitClient
{
    public partial class YobitClient
    {
        public async Task<Balance> GetUserFinancialStatus()
        {
            var response = await QueryPrivate<Balance>(new Dictionary<string, string>
            {
                ["method"] = "getInfo"
            });

            return response.Result;
        }

        public async Task<Order> AddOrder(string pair, OrderType type, decimal rate, decimal amount)
        {
            return await AddOrder(pair, type.ToString().ToLower(), rate, amount);
        }
        
        public async Task<Order> AddOrder(string pair, string type, decimal rate, decimal amount)
        {
            var response = await QueryPrivate<Order>(new Dictionary<string, string>
            {
                ["method"] = "Trade",
                ["pair"] = pair,
                ["type"] = type,
                ["rate"] = rate.ToString(Culture),
                ["amount"] = amount.ToString(Culture)
            });

            return response.Result;
        }

        public async Task<dynamic> CancelOrder(long orderId)
        {
            var response = await QueryPrivate<dynamic>(new Dictionary<string, string>
            {
                ["method"] = "CancelOrder",
                ["order_id"] = orderId.ToString(Culture)
            });

            return response.Result;
        }

        public async Task<Yobicode> RedeemYobicode(string coupon)
        {
            var response = await QueryPrivate<Yobicode>(new Dictionary<string, string>
            {
                ["method"] = "RedeemYobicode",
                ["coupon"] = coupon
            });

            return response.Result;
        }

        public async Task<OrderInfo> GetOrderInfo(long orderId)
        {
            var response = await QueryPrivate<Dictionary<string, OrderInfo>>(new Dictionary<string, string>
            {
                ["method"] = "OrderInfo",
                ["order_id"] = orderId.ToString(Culture)
            });

            return response.Result.FirstOrDefault().Value;
        }

        public async Task<YobitResponse<T>> QueryPrivate<T>(Dictionary<string, string> args)
        {
            return await QueryPrivate<T>("tapi", args);
        }

    }
}