using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace Wini.Database
{
    [Table("Shop_Product_Detail")]
    public class ProductDetail
    {
        [Key]
        public int Id { get; set; }
        //public int? ProductionCostId { get; set; }

        public int? ProductId { get; set; }
        public int? SizeId { get; set; }
        public int? ColorId { get; set; }
        public string? CodeSku { get; set; }
        public decimal Quantity { get; set; }
        public int? QuantityOrder { get; set; }

        public decimal? CreateDate { get; set; }
        public bool? IsShow { get; set; }
        //public bool? IsHot { get; set; }
        public bool? IsDelete { get; set; }

        public string? Name { get; set; }
        public int? CategoryId { get; set; }
        public string? NameAscii { get; set; }
        public string? Code { get; set; }
        //public string Author { get; set; }
        public decimal PriceOld { get; set; }
        public decimal? PriceNew { get; set; }
        //public string Format { get; set; }
        //public decimal? Weight { get; set; }
        //public int? YearOfManufacture { get; set; }
        //public int? FileBookId { get; set; }
        //public int? FIleReadtryId { get; set; }
        //public decimal? DateCreated { get; set; }
        //public int? Sort { get; set; }
        //public bool? BookOld { get; set; }
        //public bool? HasTransfer { get; set; }
        public string? Description { get; set; }
        //public string Type { get; set; }
        //public int? Ratings { get; set; }
        //public double? AvgRating { get; set; }
        //public int? AddressId { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        //public bool? IsUpcoming { get; set; }
        //public int? FreeShipFor { get; set; }
        //public int? Buyed { get; set; }
        public Size Size { get; set; }
        public Color Color { get; set; }

        public ICollection<ProductDetailPicture> Pictures { get; set; }

        [ForeignKey("ProductId")]

        public Product Product { get; set; }
  
        public Picture? Picture { get; set; }
        [NotMapped]
        public string? LstPicture { get; set; }


    }





}
