using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using YobitClient.Helpers;

namespace YobitClient.Models
{
    public class OrderBook
    {
        [JsonProperty("asks")]
        public ActiveOrder[] Asks { get; set; }

        [JsonProperty("bids")]
        public ActiveOrder[] Bids { get; set; }
    }

    [JsonConverter(typeof(JArrayToObjectConverter))]
    public class ActiveOrder
    {
        public decimal Price { get; set; }

        public decimal Volume { get; set; }

    }
}
