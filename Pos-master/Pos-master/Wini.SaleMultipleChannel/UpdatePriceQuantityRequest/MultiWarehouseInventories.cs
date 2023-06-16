using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Wini.SaleMultipleChannel.UpdatePriceQuantityRequest
{
    [XmlRoot(ElementName = "MultiWarehouseInventories")]
    public class MultiWarehouseInventories
    {

        [XmlElement(ElementName = "MultiWarehouseInventory")]
        public List<MultiWarehouseInventory> MultiWarehouseInventory { get; set; }
    }
}
