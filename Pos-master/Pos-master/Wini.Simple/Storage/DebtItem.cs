using System;
using System;
using System.Collections.Generic;
using System.Text;
using Wini.Simple.Storage;

namespace Wini.Simple
{
    public class DebtItem
    {
        public int Id { get; set; }
        public decimal? Price { get; set; }
        public decimal? DateCreate { get; set; }
        public Guid? UserCreated { get; set; }
        public string Note { get; set; }
        public int? AgencyId { get; set; }
        public decimal? Payment { get; set; }
        public IEnumerable<DebtDetailItem> DebtDetailItems { get; set; }
    }
}
