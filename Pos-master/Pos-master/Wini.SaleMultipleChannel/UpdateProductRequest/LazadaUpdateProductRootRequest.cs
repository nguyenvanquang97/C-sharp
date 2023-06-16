using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wini.SaleMultipleChannel.UpdatePriceQuantityRequest;

namespace Wini.SaleMultipleChannel.UpdateProductRequest
{
    public class LazadaUpdateProductRootRequest
    {
        [JsonProperty("Request")]
        public LazadaUpdateProductRequest Request { get; set; }
    }
}
