using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Wini.Database
{
    [Table("Debt")]
   public class Debt
    {
        public int Id { get; set; }
        public decimal? Price { get; set; }
        public decimal? DateCreate { get; set; }
        public Guid? UserCreated { get; set; }
        public string Note { get; set; }
        public int? AgencyId { get; set; }
        public decimal? Payment { get; set; }
        public ICollection<DebtDetail> DebtDetails { get; set; }
        [ForeignKey("AgencyId")]
        public Agency Agency { get; set; }

    }
}
