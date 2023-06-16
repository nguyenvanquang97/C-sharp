using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Wini.Database
{
    [Table("DN_ExportProduct")]
    public class ExportProduct
    {
        public int Id { get; set; }
        public int? AgencyId { get; set; }
        public int? AgencyIdRecive { get; set; }
        public Guid? UserId { get; set; }
        public Guid? UserGet { get; set; }
        public string Code { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? Payment { get; set; }
        public string Note { get; set; }
        public decimal? DateCreated { get; set; }
        public decimal? DateExport { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsOrder { get; set; }
        public ICollection<ImportProduct> ImportProducts { get; set; }
        public virtual ICollection<ExportProductDetail> ExportProductDetails { get; set; }
        [ForeignKey("AgencyId")]
        public Agency agency { get; set; }
        [ForeignKey("AgencyIdRecive")]
        public Agency Agency2 { get; set; }

    }
}
