using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wini.SaleMultipleChannel.Response.Lazada.CreateProduct
{
    public class LazadaSkuList
    {
        [JsonProperty("shop_sku")]
        public string ShopSku { get; set; }
        [JsonProperty("seller_sku")]
        public string SellerSku { get; set; }
        [JsonProperty("sku_id")]
        public string SkuId { get; set; }
    }

}
