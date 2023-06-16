using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Wini.SaleMultipleChannel.CreateProductRequest;
using Wini.SaleMultipleChannel.Response.Lazada.Product;

namespace Wini.SaleMultipleChannel.UpdateProductRequest
{
    public class LazadaUpdateProductSku
    {
        [JsonProperty("SellerSku")]
        public string SellerSku { get; set; }
        [JsonProperty("quantity")]
        public string Quantity { get; set; }
        [JsonProperty("price")]
        public string Price { get; set; }
        [JsonProperty("special_price")]
        public string SpecialPrice { get; set; }
        [JsonProperty("special_from_date")]
        public string SpecialFromDate { get; set; }
        [JsonProperty("special_to_date")]
        public string SpecialToDate { get; set; }
        [JsonProperty("package_height")]
        public string PackageHeight { get; set; }
        [JsonProperty("package_length")]
        public string PackageLength { get; set; }
        [JsonProperty("package_width")]
        public string PackageWidth { get; set; }
        [JsonProperty("package_weight")]
        public string PackageWeight { get; set; }
        [JsonProperty("Status")]
        public string Status { get; set; }
        [JsonProperty("Images")]
        public LazadaUpdateProductImages Images { get; set; }

    }
}
