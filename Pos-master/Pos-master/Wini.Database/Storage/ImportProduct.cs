using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Wini.Database
{
    [Table("DN_ImportProduct")]
   public class ImportProduct
    {
        [Key]
        public System.Guid Gid { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? QuantityOut { get; set; }
        public decimal? Price { get; set; }
        public bool? IsDelete { get; set; }
        public decimal? Date { get; set; }
        public decimal? DateEnd { get; set; }
        public string BarCode { get; set; }
        public decimal? PriceNew { get; set; }
        public decimal? Value { get; set; }
        public int? ProductValueId { get; set; }
        public int? AgencyId { get; set; }
        public bool? Ischeck { get; set; }
        public Guid? UserCreated { get; set; }
        public Guid? UserUpdate { get; set; }
        public decimal? CreateDate { get; set; }
        public decimal? UpdateDate { get; set; }
        public int? CustomerId { get; set; }
        public int? ProductId { get; set; }
        public int? NccId { get; set; }
        public decimal? DateEx { get; set; }
        public ProductDetail ProductDetail { get; set; }
    }
}
