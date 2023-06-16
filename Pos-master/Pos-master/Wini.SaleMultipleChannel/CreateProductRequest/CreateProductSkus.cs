using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wini.SaleMultipleChannel.CreateProductRequest

{
    public class CreateProductSkus
    {
        public CreateProductSkus()
        {
        }

        public CreateProductSkus(List<LazadaCreateProductSku> sku)
        {
            Sku = sku;
        }

        public List<LazadaCreateProductSku> Sku { get; set; }
    }

}

