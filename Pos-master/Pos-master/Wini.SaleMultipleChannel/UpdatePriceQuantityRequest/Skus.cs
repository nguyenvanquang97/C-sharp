using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Wini.SaleMultipleChannel.UpdatePriceQuantityRequest
{

    [XmlRoot(ElementName = "Skus")]
    public class Skus
    {

        [XmlElement(ElementName = "Sku")]
        public Sku Sku { get; set; }
    }

}
