using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Wini.Database
{
    [Table("Export_Product")]
   public class ExportProductDetail
    {
        public int Id { get; set; }
        public int? ExportId { get; set; }
        public Guid? InportProductId { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? Date { get; set; }
        public bool? IsDelete { get; set; }
        [ForeignKey("InportProductId")]
        public ImportProduct ImportProduct { get; set; }
    }
}
