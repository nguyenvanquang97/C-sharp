using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wini.SaleMultipleChannel.Response.Lazada.Product
{
    public class LazadaAttributesResponse
    {
        [JsonProperty("short_description")]
        public string ShortDescription { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("Description")]
        public string Description { get; set; }
        [JsonProperty("brand")]
        public string Brand { get; set; }
        [JsonProperty("glassware_everyday_type")]
        public string GlasswareEverydayType { get; set; }
        [JsonProperty("warranty_type")]
        public string WarrantyType { get; set; }
        [JsonProperty("name_en")]
        public string NameEn { get; set; }
        [JsonProperty("warranty")]
        public string Warranty { get; set; }
        [JsonProperty("Hazmat")]
        public string Hazmat { get; set; }
        [JsonProperty("source")]
        public string Source { get; set; }
        [JsonProperty("delivery_option_sof")]
        public string DeliveryOptionSof { get; set; }
    }
}
