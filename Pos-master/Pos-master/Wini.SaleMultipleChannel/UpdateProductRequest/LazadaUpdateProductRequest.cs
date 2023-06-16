using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Wini.SaleMultipleChannel.UpdateProductRequest
{
    public class LazadaUpdateProductRequest
    {
        [JsonProperty("Product")]             
        
        public LazadaUpdateProduct Product { get; set; }
    }

}
