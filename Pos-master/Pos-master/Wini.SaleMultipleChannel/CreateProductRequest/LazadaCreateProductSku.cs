using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Wini.SaleMultipleChannel.CreateProductRequest

{
    public class LazadaCreateProductSku
    {
        public LazadaCreateProductSku()
        {
        }

        public LazadaCreateProductSku(string sellerSku, string quantity, string price, string special_price, string special_from_date, string special_to_date, string package_height, string package_length, string package_width, string package_weight, LazadaCreatProductImages images)
        {
            SellerSku = sellerSku;
            Quantity = quantity;
            Price = price;
            Special_price = special_price;
            Special_from_date = special_from_date;
            Special_to_date = special_to_date;
            Package_height = package_height;
            Package_length = package_length;
            Package_width = package_width;
            Package_weight = package_weight;
            Images = images;
        }

        [JsonProperty("SellerSku")]
        public string SellerSku { get; set; }
        [JsonProperty("quantity")]
        public string Quantity { get; set; }
        [JsonProperty("price")]
        public string Price { get; set; }
        [JsonProperty("special_price")]
        public string Special_price { get; set; }
        [JsonProperty("special_from_date")]
        public string Special_from_date { get; set; }
        [JsonProperty("special_to_date")]
        public string Special_to_date { get; set; }
        [JsonProperty("package_height")]
        public string Package_height { get; set; }
        [JsonProperty("package_length")]
        public string Package_length { get; set; }
        [JsonProperty("package_width")]
        public string Package_width { get; set; }
        [JsonProperty("package_weight")]
        public string Package_weight { get; set; }
        [JsonProperty("Images")]
        public LazadaCreatProductImages Images { get; set; }
    }
}
