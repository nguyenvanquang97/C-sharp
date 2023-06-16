using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Wini.SaleMultipleChannel.UpdatePriceQuantityRequest
{
    [XmlRoot(ElementName = "Request")]
    public class Request
    {

        [XmlElement(ElementName = "Product")]
        public Product Product { get; set; }
    }
}
