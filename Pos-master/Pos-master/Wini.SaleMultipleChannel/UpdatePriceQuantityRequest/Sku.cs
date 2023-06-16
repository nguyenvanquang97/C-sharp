using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Wini.SaleMultipleChannel.UpdatePriceQuantityRequest
{
    [XmlRoot(ElementName = "Sku")]
    public class Sku
    {

        [XmlElement(ElementName = "ItemId")]
        public int ItemId { get; set; }

        [XmlElement(ElementName = "SkuId")]
        public int SkuId { get; set; }

        [XmlElement(ElementName = "SellerSku")]
        public string SellerSku { get; set; }

        [XmlElement(ElementName = "Price")]
        public double Price { get; set; }

        [XmlElement(ElementName = "SalePrice")]
        public double SalePrice { get; set; }

        [XmlElement(ElementName = "SaleStartDate")]
        public DateTime SaleStartDate { get; set; }

        [XmlElement(ElementName = "SaleEndDate")]
        public DateTime SaleEndDate { get; set; }

        [XmlElement(ElementName = "MultiWarehouseInventories")]
        public MultiWarehouseInventories MultiWarehouseInventories { get; set; }
    }
}
