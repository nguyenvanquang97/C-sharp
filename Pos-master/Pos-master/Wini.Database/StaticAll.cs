using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Wini.Database
{
    
    public class StaticAll
    {
        public int I { get; set; }
        public decimal? TotalPriceImport { get; set; }
        public decimal? TotalPriceExport { get; set; }
        public decimal? TotalPriceDebt { get; set; }
        public decimal? TotalPriceDebtPayment { get; set; }

    }
}
