using System;
using System.Collections.Generic;
using System.Text;

namespace Wini.Simple
{
    public class BrandItem
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int? PictureId { get; set; }
        public string PictureUrl { get; set; }
        public string Phone { get; set; }
        public string TaxCode { get; set; }
        public string Note { get; set; }
        public bool? IsShow { get; set; }
        public string LanguageId { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
