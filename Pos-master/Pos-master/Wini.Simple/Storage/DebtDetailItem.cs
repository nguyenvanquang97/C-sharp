using System;
using System.Collections.Generic;
using System.Text;
using Wini.Simple.Storage;

namespace Wini.Simple
{
    public class DebtDetailItem
    {
        public int Id { get; set; }
        public decimal? Price { get; set; }
        public decimal? DebtId { get; set; }
        public decimal? DateCreate { get; set; }
        public string Note { get; set; }
    }
}
