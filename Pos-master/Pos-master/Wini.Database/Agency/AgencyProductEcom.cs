using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wini.Database
{
    [Table("AgencyProductEcom")]
    public class AgencyProductEcom
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int AgencyId { get; set; }
        public int TypeId { get; set; }
        public decimal Price { get; set; }
        public string SellerSku { get; set; }
        public string? Brand { get; set;}
    }
}
