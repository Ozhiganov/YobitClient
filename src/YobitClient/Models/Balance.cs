using Newtonsoft.Json;

namespace YobitClient.Models
{
    public class Balance
    {
        [JsonProperty("rights")]
        public Rights Rights { get; set; }

        [JsonProperty("funds")]
        public dynamic Funds { get; set; }

        [JsonProperty("funds_incl_orders")]
        public dynamic FundsWithOrders { get; set; }

        [JsonProperty("transaction_count")]
        public long TransactionCount { get; set; }

        [JsonProperty("open_orders")]
        public long OpenOrders { get; set; }

        [JsonProperty("server_time")]
        public long ServerTime { get; set; }
    }

    public class Funds
    {
        [JsonProperty("btc")]
        public double Btc { get; set; }

        [JsonProperty("liza")]
        public long Liza { get; set; }

        [JsonProperty("planet")]
        public long Planet { get; set; }

        [JsonProperty("smart")]
        public double Smart { get; set; }

        [JsonProperty("btv")]
        public double Btv { get; set; }

        [JsonProperty("frwc")]
        public long Frwc { get; set; }

        [JsonProperty("bronz")]
        public long Bronz { get; set; }

        [JsonProperty("tng")]
        public long Tng { get; set; }

        [JsonProperty("pac")]
        public long Pac { get; set; }

        [JsonProperty("vapor")]
        public long Vapor { get; set; }

        [JsonProperty("spx")]
        public long Spx { get; set; }

        [JsonProperty("rur")]
        public double Rur { get; set; }
    }

    public class Rights
    {
        [JsonProperty("info")]
        public long Info { get; set; }

        [JsonProperty("trade")]
        public long Trade { get; set; }

        [JsonProperty("deposit")]
        public long Deposit { get; set; }

        [JsonProperty("withdraw")]
        public long Withdraw { get; set; }
    }
}