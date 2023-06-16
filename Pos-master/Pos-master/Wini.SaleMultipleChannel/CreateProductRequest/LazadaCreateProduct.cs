using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wini.SaleMultipleChannel.CreateProductRequest

{
    public class LazadaCreateProduct
    {
        public LazadaCreateProduct()
        {
        }

        public LazadaCreateProduct(string primaryCategory, LazadaCreatProductImages images, LazadaCreateProductAttributes attributes, CreateProductSkus skus)
        {
            PrimaryCategory = primaryCategory;
            Images = images;
            Attributes = attributes;
            Skus = skus;
        }

        public string PrimaryCategory { get; set; }
        public LazadaCreatProductImages Images { get; set; }
        public LazadaCreateProductAttributes Attributes { get; set; }
        public CreateProductSkus Skus { get; set; }
    }
}
