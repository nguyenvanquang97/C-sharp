using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Wini.SaleMultipleChannel.UpdateProductRequest
{
    public class LazadaUpdateProductSkus
    {

        [XmlElement(ElementName = "Sku")]
        public List<LazadaUpdateProductSku> Sku { get; set; }
    }
}
