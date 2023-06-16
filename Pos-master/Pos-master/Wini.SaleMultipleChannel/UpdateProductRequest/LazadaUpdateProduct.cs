using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Wini.SaleMultipleChannel.Model.Product;
using Wini.SaleMultipleChannel.UpdatePriceQuantityRequest;

namespace Wini.SaleMultipleChannel.UpdateProductRequest
{
    public class LazadaUpdateProduct
    {
        [JsonProperty("PrimaryCategory")]
        public string PrimaryCategory { get; set; }
        [JsonProperty("AssociatedSku")]
        public string AssociatedSku { get; set; }
        [JsonProperty("Images")]
        public LazadaUpdateProductImages Images { get; set; }
        [JsonProperty("Attributes")]
        public LazadaUpdateProductAttributes Attributes { get; set; }
        [JsonProperty("Skus")]
        public LazadaUpdateProductSkus Skus { get; set; }
    }
}
