using Newtonsoft.Json;

namespace YobitClient.Models
{
    public class Order
    {
        [JsonProperty("order_id")]
        public long OrderId { get; set; }

        [JsonProperty("received")]
        public decimal Received { get; set; }

        [JsonProperty("remains")]
        public decimal Remains { get; set; }

        [JsonProperty("funds")]
        public dynamic Funds { get; set; }

        [JsonProperty("funds_incl_orders")]
        public dynamic FundsWithOrders { get; set; }

        [JsonProperty("server_time")]
        public long ServerTime { get; set; }
    }
}
