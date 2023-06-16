using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Wini.SaleMultipleChannel.UpdatePriceQuantityRequest
{
    [XmlRoot(ElementName = "Product")]
    public class Product
    {

        [XmlElement(ElementName = "Skus")]
        public Skus Skus { get; set; }
    }

}
