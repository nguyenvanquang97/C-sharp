using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wini.SaleMultipleChannel.Response.Lazada.Product;

namespace Wini.SaleMultipleChannel.Model.Product
{
    public class Sku
    {
        public string Status { get; set; }

        public int Quantity { get; set; }
        public List<string> Images { get; set; }
        public string SellerSku { get; set; }
        public string ShopSku { get; set; }

        public string Url { get; set; }
        public string PackageWidth { get; set; }
        public string PackageHeight { get; set; }
        public double SpecialPrice { get; set; }
        public double Price { get; set; }
        public string PackageLength { get; set; }
        public string PackageWeight { get; set; }
        public long SkuId { get; set; }
    }
}
