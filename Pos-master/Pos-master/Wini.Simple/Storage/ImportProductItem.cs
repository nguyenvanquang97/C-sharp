using System;
using System.Collections.Generic;
using System.Text;

namespace Wini.Simple.Storage
{
    public class ImportProductItem
    {
        public int? Stt { get; set; }
        public Guid Gid { get; set; }
        public decimal? Date { get; set; }
        public decimal? DateEnd { get; set; }
        public string Code { get; set; }
        public string Imei { get; set; }
        public string BarCode { get; set; }
        public string Name { get; set; }
        public string CodeSku { get; set; }
        public string ColorName { get; set; }
        public string SizeName { get; set; }
        public decimal? Value { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? QuantityOut { get; set; }
        public decimal? Price { get; set; }
        public bool? IsDelete { get; set; }
        public bool IsIn { get; set; }
        public bool IsDate { get; set; }
        public decimal? Weight { get; set; }
        public decimal? PriceNew { get; set; }
        public string UrlPicture { get; set; }
        public string UnitName { get; set; }
        public int? UnitId { get; set; }
        public int? AgencyId { get; set; }
        public string Description { get; set; }
        public int? ProductId { get; set; }
    }
}
