using System;
using System.Collections.Generic;
using System.Text;

namespace Wini.Simple
{
   public class AgencyTypeItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? PriceDebt { get; set; }
        public string Icon { get; set; }
        public decimal? TotalPayment { get; set; }
        public int? Discount { get; set; }

    }
}
