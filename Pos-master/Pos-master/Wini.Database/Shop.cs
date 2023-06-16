
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wini.Database
{
    [Table("Shop")]
    public class Shop
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("ShopId")]
        public ICollection<Employment> Employments { get; set; }
    }

}
