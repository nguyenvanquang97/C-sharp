using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Wini.Database
{

    [Table("Shop_Product_Picture")]
    public class ProductDetailPicture
    {
        public ProductDetailPicture()
        {

        }
        [Key]

        public int PictureId { get; set; }

        public int ProductId { get; set; }
        public int? Sort { get; set; }

        [ForeignKey("PictureId")]

        public virtual Picture Picture { get; set; }

        [ForeignKey("ProductId")]

        public virtual ProductDetail ProductDetail { get; set; }
    }

}
