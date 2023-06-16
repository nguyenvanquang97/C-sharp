using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Wini.SaleMultipleChannel.UpdatePriceQuantityRequest
{

    [XmlRoot(ElementName = "MultiWarehouseInventory")]
    public class MultiWarehouseInventory
    {

        [XmlElement(ElementName = "WarehouseCode")]
        public string WarehouseCode { get; set; }

        [XmlElement(ElementName = "Quantity")]
        public int Quantity { get; set; }
    }

}
