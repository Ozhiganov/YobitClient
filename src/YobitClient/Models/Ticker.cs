using Newtonsoft.Json;

namespace YobitClient.Models
{
    public class Ticker
    {
        [JsonProperty("avg")]
        public decimal Average { get; set; }

        [JsonProperty("buy")]
        public decimal Buy { get; set; }

        [JsonProperty("high")]
        public decimal High { get; set; }

        [JsonProperty("last")]
        public decimal Last { get; set; }

        [JsonProperty("low")]
        public decimal Low { get; set; }

        [JsonProperty("sell")]
        public decimal Sell { get; set; }

        [JsonProperty("vol")]
        public decimal Volume { get; set; }

        [JsonProperty("vol_cur")]
        public decimal VolumeCurrent { get; set; }

        [JsonProperty("server_time")]
        public uint ServerTime { get; set; }

        public CurrencyPair CurrencyPair { get; set; }
    }
}