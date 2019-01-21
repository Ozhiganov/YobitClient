using Newtonsoft.Json;

namespace YobitClient.Models
{
    public class Yobicode
    {
        [JsonProperty("couponAmount")]
        public string CouponAmount { get; set; }

        [JsonProperty("couponCurrency")]
        public string CouponCurrency { get; set; }

        [JsonProperty("transID")]
        public long TransactionId { get; set; }

        [JsonProperty("funds")]
        public dynamic Funds { get; set; }
    }
}
