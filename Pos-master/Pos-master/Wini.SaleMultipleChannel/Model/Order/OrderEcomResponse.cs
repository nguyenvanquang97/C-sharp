using DocumentFormat.OpenXml.Drawing.Charts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wini.SaleMultipleChannel.Response.Lazada.Order;

namespace Wini.SaleMultipleChannel.Model.Order
{
    public class OrderEcomResponse
    {
       
        public string Count { get; set; }
   
        public string CountTotal { get; set; }
      
        public List<OrderEcom> Orders { get; set; }
    }
}
