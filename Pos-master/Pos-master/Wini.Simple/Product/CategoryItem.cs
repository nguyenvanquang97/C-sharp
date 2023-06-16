using System;
using System.Collections.Generic;
using System.Text;

namespace Wini.Simple
{
    public class CategoryItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameRoot { get; set; }
        public string Slug { get; set; }
        public int? ParentId { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public int? IsLevel { get; set; }
        public int? Type { get; set; }
        public bool? IsShow { get; set; }
        public bool? IsHome { get; set; }
        public bool? IsMenu { get; set; }
        public bool? IsDeleted { get; set; }
        public int? Sort { get; set; }
        public int? AgencyId { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeyword { get; set; }
        public string LanguageId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UserCreate { get; set; }
        public string UrlPicture { get; set; }
        public int? PictureId { get; set; }
    }
}
