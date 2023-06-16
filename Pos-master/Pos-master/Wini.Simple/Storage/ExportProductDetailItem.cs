using System;
using System.Collections.Generic;
using System.Text;

namespace Wini.Simple
{
    public class ExportProductDetailItem
    {
        public int Id { get; set; }
        public int? ExportId { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? QuantityOut { get; set; }
        public decimal? Price { get; set; }
        public decimal? Date { get; set; }
        public decimal? DateEnd { get; set; }
        public string ProductName { get; set; }
        public string Barcode { get; set; }
        public string ProductCode { get; set; }
        public string Sizename { get; set; }
        public string Color { get; set; }
        public string UnitName { get; set; }
        public int? ProductId { get; set; }
        public int? ImportId { get; set; }
        public bool? IsDelete { get; set; }

    }
}
