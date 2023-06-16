using System;
using System.Collections.Generic;
using System.Text;

namespace Wini.Simple
{
    public class ProductDetailItem
    {

        public int Id { get; set; }
        public int? AgencyId { get; set; }
        public string UrlPicture { get; set; }
        public string Name { get; set; }
        public string CodeSku { get; set; }
        public decimal? Value { get; set; }
        public int? CateId { get; set; }
        public string NameAscii { get; set; }
        public string NameCate { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
        public decimal? PriceCost { get; set; }
        public decimal? PriceOld { get; set; }
        public decimal PriceNew { get; set; }
        public double? SizeValue { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Percent { get; set; }
        public int? QuantityDay { get; set; }
        public string CreateBy { get; set; }
        public int? TypeId { get; set; }
        public string BarCode { get; set; }
        public decimal? CreateDate { get; set; }
        public decimal? DateEnd { get; set; }
        public bool? IsShow { get; set; }
        public bool? IsHot { get; set; }
        public string LanguageId { get; set; }
        public bool? IsDelete { get; set; }
        public int? UnitId { get; set; }
        public int? SupplierId { get; set; }
        public int? HomeId { get; set; }
        public int? SizeId { get; set; }
        public int? ColorId { get; set; }
        public int? BrandId { get; set; }
        public int? ProductionCostId { get; set; }
        public int? ProductDetailId { get; set; }
        public string LstSort { get; set; }
        public int? QuantityOut { get; set; }

        public IEnumerable<PictureItem> LstPictures { get; set; }
        public string SizeName { get; set; }
        public string ColorName { get; set; }
        public string UnitName { get; set; }
        public string ProductName { get; set; }
        public decimal? PriceCostParent { get; set; }
        public decimal? PriceInCurr { get; set; }
        public CategoryItem CategoryItem { get; set; }
        public decimal? PriceUnit { get; set; }  // //don gia
        public int ProductId { get; set; }
    }
    

}
