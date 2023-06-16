using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Wini.Database
{
    [Table("Debt_Detail")]
   public class DebtDetail
    {
        public int Id { get; set; }
        public decimal? Price { get; set; }
        public decimal? DebtId { get; set; }
        public decimal? DateCreate { get; set; }
        public string Note { get; set; }
        public Debt Debt { get; set; }
        //update nâng cấp thêm file đính kèm
    }
}
