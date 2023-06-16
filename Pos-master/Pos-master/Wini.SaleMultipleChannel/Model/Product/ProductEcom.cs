using Newtonsoft.Json;

namespace Wini.SaleMultipleChannel.Model.Product
{
    public partial class SaleLazada
    {
        public class ProductEcom
        {
            public string CreatedTime { get; set; }

            public string UpdatedTime { get; set; }
            public List<string> Images { get; set; }
            public List<Sku> Skus { get; set; }
            public long ItemId { get; set; }
            public int PrimaryCategory { get; set; }
            public Attributes Attributes { get; set; }
            public string Status { get; set; }

        }
    }
}