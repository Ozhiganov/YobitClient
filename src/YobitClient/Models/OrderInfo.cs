using Newtonsoft.Json;

namespace YobitClient.Models
{
    public class OrderInfo
    {
        [JsonProperty("pair")]
        public string Pair { get; set; }

        [JsonProperty("type")]
        public string PurpleType { get; set; }

        [JsonProperty("start_amount")]
        public decimal StartAmount { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("rate")]
        public decimal Rate { get; set; }

        [JsonProperty("timestamp_created")]
        public long TimestampCreated { get; set; }

        [JsonProperty("status")]
        public long Status { get; set; }
    }
}