using System;
using System.Collections.Generic;
using System.Text;

namespace Wini.Simple
{
    public class ExportProductItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? AgencyId { get; set; }
        public string Code { get; set; }
        public decimal? DateCreated { get; set; }
        public string Note { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? Payment { get; set; }
        public Guid? UserId { get; set; }
        public Guid? UserGet { get; set; }
        public string UserName { get; set; }
        public string UserGetName { get; set; }
        public decimal? DateExport { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsOrder { get; set; }
        public virtual IEnumerable<ExportProductDetailItem> ExportProductDetailItems { get; set; }

    }
}
