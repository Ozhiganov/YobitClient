using System;
using Newtonsoft.Json;

namespace YobitClient.Models
{
    public class Trade
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("tid")]
        public long Id { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonIgnore]
        public DateTime DateAndTIme => DateTimeOffset.FromUnixTimeSeconds(Timestamp).DateTime.ToLocalTime();

    }
}
