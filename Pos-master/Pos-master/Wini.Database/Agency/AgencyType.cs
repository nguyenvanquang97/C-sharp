using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Wini.Database
{
    [Table("Agency_Type")]
    public class AgencyType
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? PriceDebt { get; set; }
        public string? Icon { get; set; }
        public decimal? TotalPayment { get; set; }
        public int? Discount { get; set; }
        public bool? IsShow { get; set; }
        public bool? IsDeleted { get; set; }

    }
}
