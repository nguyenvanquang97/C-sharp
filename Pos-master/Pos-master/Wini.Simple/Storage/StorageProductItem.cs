using System;
using System.Collections.Generic;
using System.Text;
using Wini.Simple.Storage;

namespace Wini.Simple
{
    public class StorageProductItem
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public decimal? DateCreated { get; set; }
        public decimal? DateImport { get; set; }
        public Guid? UserId { get; set; }
        public string UserName { get; set; }
        public string Note { get; set; }
        public int? AgencyId { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? Payment { get; set; }
        public bool? IsDelete { get; set; }
        public bool? IsOrder { get; set; }
        public int? CateId { get; set; }
        public int? SupId { get; set; }
        public int? TotalId { get; set; }
        public int? Hour { get; set; }
        public int? HourImport { get; set; }
        public int? Status { get; set; }
        public int? Type { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public string Suppliername { get; set; }
        public string Catename { get; set; }
        public decimal? Today { get; set; }
        public int? Hours { get; set; }
        public IEnumerable<ImportProductItem> ImportProductItem { get; set; }
    }
}
