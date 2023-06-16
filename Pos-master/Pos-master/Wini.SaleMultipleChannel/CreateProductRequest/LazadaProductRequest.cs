using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wini.SaleMultipleChannel.CreateProductRequest

{
    public class LazadaProductRequest
    {
        public LazadaProductRequest()
        {
        }
        public LazadaProductRequest(LazadaRequest request)
        {
            Request = request;
        }

        public LazadaRequest Request { get; set; }
    }
}
