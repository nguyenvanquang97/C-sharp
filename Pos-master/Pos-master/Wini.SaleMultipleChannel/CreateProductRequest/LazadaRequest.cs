using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wini.SaleMultipleChannel.CreateProductRequest

{
    public class LazadaRequest
    {
        public LazadaRequest()
        {
        }

        public LazadaRequest(LazadaCreateProduct product)
        {
            Product = product;
        }

        public LazadaCreateProduct Product { get; set; }
    }
}
