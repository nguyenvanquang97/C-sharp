using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wini.SaleMultipleChannel.Response.Lazada.Token
{
    public class LazadaCountryUserInfo
    {
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("user_id")]
        public string UserId { get; set; }
        [JsonProperty("seller_id")]
        public string SellerId { get; set; }
        [JsonProperty("short_code")]
        public string ShortCode { get; set; }
    }

}
