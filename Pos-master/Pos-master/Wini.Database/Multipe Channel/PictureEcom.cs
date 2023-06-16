using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wini.Database.Multipe_Channel
{
    [Table("PictureEcom")]
    public class PictureEcom
    {
        public int Id { get; set; }
        public int PictureId { get; set; }
        public string? LazadaImage { get; set; }
        public string? ShopeeImage { get; set; }
        public string? TikiImage { get; set; }
    }
}
