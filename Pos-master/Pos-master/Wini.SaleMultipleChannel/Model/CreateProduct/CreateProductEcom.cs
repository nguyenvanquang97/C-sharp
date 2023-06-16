using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wini.SaleMultipleChannel.Response.Lazada.CreateProduct;

namespace Wini.SaleMultipleChannel.Model.CreateProduct
{
    public class CreateProductEcom
    {
        public string ItemId { get; set; }
        public List<SkuList> SkuList { get; set; }
    }
}
