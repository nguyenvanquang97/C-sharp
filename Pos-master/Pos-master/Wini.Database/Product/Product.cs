using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Wini.Database
{
    [Table("Shop_Product")]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? code { get; set; }
        public int? QuantityDay { get; set; }
        public decimal? StartDate { get; set; }
        public int? Minutes { get; set; }
        public decimal? EndDate { get; set; }
        public decimal Price { get; set; }
        public decimal? PriceCost { get; set; }
        public decimal? PriceOld { get; set; }
        public int? Sale { get; set; }
        public string? NameAscii { get; set; }
        public string? Description { get; set; }
        public string? Details { get; set; }
        public int? UnitId { get; set; }
        //public int? AgencyId { get; set; }
        public bool? IsHot { get; set; }
        public bool? IsShow { get; set; }
        public int PictureId { get; set; }
        public int? Sort { get; set; }
        public bool? IsDelete { get; set; }
        public string? SeoTitle { get; set; }
        public string? SeoDescription { get; set; }
        public string? SeoKeyword { get; set; }
        public int? CateId { get; set; }
        public decimal? DateCreate { get; set; }
        public decimal? Percent { get; set; }
        public decimal? Value { get; set; }
        public Guid? UserId { get; set; }
        public decimal? DateUpdate { get; set; }
        public Picture Picture { get; set; }
        public Unit? Unit { get; set; }


    }



}
