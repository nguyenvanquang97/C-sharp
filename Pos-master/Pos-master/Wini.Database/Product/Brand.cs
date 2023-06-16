using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wini.Database
{

    [Table("Shop_Brands")]
    public class Brand
    {
        public Brand()
        {

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameAscii { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public int? Order { get; set; }
        public bool? IsShow { get; set; }
        public int? PictureId { get; set; }
        public int? LogoPictureId { get; set; }
        public string LanguageId { get; set; }
        public int? ParentId { get; set; }
        public bool? IsDeleted { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string TaxCode { get; set; }
        public string Note { get; set; }


    }

}
