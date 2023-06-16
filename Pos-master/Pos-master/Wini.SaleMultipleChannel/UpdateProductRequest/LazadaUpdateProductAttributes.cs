using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Wini.SaleMultipleChannel.UpdateProductRequest
{
    public class LazadaUpdateProductAttributes
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("brand")]
        public string Brand { get; set; }
        [JsonProperty("waterproof")]
        public string Waterproof { get; set; }
        [JsonProperty("warranty_type")]
        public string WarrantyType { get; set; }
        [JsonProperty("warranty")]
        public string Warranty { get; set; }
        [JsonProperty("short_description")]
        public string ShortDescription { get; set; }
        [JsonProperty("Hazmat")]
        public string Hazmat { get; set; }
        [JsonProperty("material")]
        public string Material { get; set; }
        [JsonProperty("delivery_option_sof")]
        public string DeliveryOptionSof { get; set; }
        [JsonProperty("remove_video")]
        public string RemoveVideo { get; set; }
    }
}
