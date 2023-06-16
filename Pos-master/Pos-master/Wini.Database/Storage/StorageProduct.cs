using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Wini.Database
{
    [Table("StorageProduct")]
   public class StorageProduct
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public decimal? DateCreated { get; set; }
        public Guid? UserId { get; set; }
        public string Note { get; set; }
        public int? AgencyId { get; set; }
        public decimal? TotalPrice { get; set; }
        public bool? IsDelete { get; set; }
        public decimal? DateImport { get; set; }
        public int? Quantity { get; set; }
        public int? SupId { get; set; }
        public decimal? Today { get; set; }
        public int? Hour { get; set; }
        public int? CateId { get; set; }
        public int? TotalId { get; set; }
        public int? HoursImport { get; set; }
        public int? AreaId { get; set; }
        public int? Status { get; set; }
        public int? Type { get; set; }
        public ICollection<ImportProduct> ImportProducts { get; set; }
    }
}
