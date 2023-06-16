using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wini.SaleMultipleChannel.Response.Lazada.Order
{
    public class LazadaOrderResponse
    {
        [JsonProperty("count")]
        public string Count { get; set; }
        [JsonProperty("countTotal")]
        public string CountTotal { get; set; }
        [JsonProperty("orders")]
        public List<LazadaOrder> Orders { get; set; }
    }
}
