using System.ComponentModel.DataAnnotations.Schema;

namespace Wini.Database
{
    [Table("Employment")]
    public class Employment
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int ShopId { get; set; }
        public Shop Standard { get; set; }
    }

}
