using DocumentFormat.OpenXml.ExtendedProperties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wini.SaleMultipleChannel.Response.Lazada.Product
{
    public class LazadaSku
    {
        [JsonProperty("Status")]
        public string Status { get; set; }
        [JsonProperty("quantity")]
        public int Quantity { get; set; }
        [JsonProperty("Images")]
        public List<string> Images { get; set; }
        [JsonProperty("SellerSku")]
        public string SellerSku { get; set; }
        [JsonProperty("ShopSku")]
        public string ShopSku { get; set; }
        [JsonProperty("saleProp")]
        public SaleProp saleProp { get; set; }
        [JsonProperty("Url")]
        public string Url { get; set; }
        [JsonProperty("multiWarehouseInventories")]
        public List<MultiWarehouseInventory> MultiWarehouseInventories { get; set; }
        [JsonProperty("package_width")]
        public string PackageWidth { get; set; }
        [JsonProperty("package_height")]
        public string PackageHeight { get; set; }
        [JsonProperty("fblWarehouseInventories")]
        public List<object> FblWarehouseInventories { get; set; }
        [JsonProperty("special_price")]
        public double SpecialPrice { get; set; }
        [JsonProperty("price")]
        public double Price { get; set; }
        [JsonProperty("channelInventories")]
        public List<object> ChannelInventories { get; set; }
        [JsonProperty("package_length")]
        public string PackageLength { get; set; }
        [JsonProperty("package_weight")]
        public string PackageWeight { get; set; }
        [JsonProperty("SkuId")]
        public long SkuId { get; set; }
    }
}
