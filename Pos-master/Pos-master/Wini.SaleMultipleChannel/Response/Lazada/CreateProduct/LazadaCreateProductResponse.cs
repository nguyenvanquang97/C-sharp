using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wini.SaleMultipleChannel.Response.Lazada.CreateProduct
{
    public class LazadaCreateProductResponse
    {
        [JsonProperty("item_id")]
        public string ItemId { get; set; }
        [JsonProperty("sku_list")]
        public List<LazadaSkuList> SkuList { get; set; }
    }
}
