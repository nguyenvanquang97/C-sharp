using System;
using System.Collections.Generic;
using System.Text;

namespace Wini.Simple
{
    public class ProductItem
    {

        public int Id { get; set; }
        public decimal? QuantityDay { get; set; }
        public int? Quantity { get; set; }
        public decimal? StartDate { get; set; }
        public decimal? Price { get; set; }
        public string Shopname { get; set; }
        public decimal? Percent { get; set; }
        public string Name { get; set; }
        public string NameAscii { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
        public int? UnitId { get; set; }
        public bool? IsHot { get; set; }
        public bool? IsShow { get; set; }
        public int? PictureId { get; set; }
        public int? Sort { get; set; }
        public string UrlPicture { get; set; }
        public string Code { get; set; }
        
        public string UnitName { get; set; }
        public int? Sale { get; set; }
        public int? CategoryId { get; set; }
        public decimal? Value { get; set; }
        public string Catename { get; set; }
        public decimal? DateCreate { get; set; }
    }
    

}
