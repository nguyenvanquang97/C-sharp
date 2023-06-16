using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wini.SaleMultipleChannel.CreateProductRequest
{
    public class LazadaCreateProductAttributes
    {
        public LazadaCreateProductAttributes()
        {
        }

        public LazadaCreateProductAttributes(string name, string description, string brand, string model, string warranty_type, string warranty, string hazmat, string material, string delivery_option_sof)
        {
            Name = name;
            Description = description;
            Brand = brand;
            Model = model;
            Warranty_type = warranty_type;
            Warranty = warranty;
            Hazmat = hazmat;
            Material = material;
            Delivery_option_sof = delivery_option_sof;
        }

        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("brand")]
        public string Brand { get; set; }
        [JsonProperty("model")]
        public string Model { get; set; }
        [JsonProperty("warranty_type")]
        public string Warranty_type { get; set; }
        [JsonProperty("warranty")]
        public string Warranty { get; set; }
        [JsonProperty("Hazmat")]
        public string Hazmat { get; set; }
        [JsonProperty("material")]
        public string Material { get; set; }
        [JsonProperty("delivery_option_sof")]
        public string Delivery_option_sof { get; set; }
    }
}
