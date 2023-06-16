using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wini.SaleMultipleChannel.Response.Lazada.Product
{
    public class MultiWarehouseInventory
    {

        [JsonProperty("occupyQuantity")]
            
        public int OccupyQuantity { get; set; }
        [JsonProperty("quantity")]
        public int Quantity { get; set; }
        [JsonProperty("totalQuantity")]
        public int TotalQuantity { get; set; }
        [JsonProperty("withholdQuantity")]
        public int WithholdQuantity { get; set; }
        [JsonProperty("warehouseCode")]
        public string WarehouseCode { get; set; }
        [JsonProperty("sellableQuantity")]
        public int sellableQuantity { get; set; }
    }

}
