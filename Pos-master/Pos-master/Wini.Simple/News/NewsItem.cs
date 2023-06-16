using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wini.Simple
{
    public class NewsItem
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string TitleAscii { get; set; }
        public int? PictureID { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
        public System.DateTime? DateCreated { get; set; }
        public System.DateTime? DateUpdated { get; set; }
        public int? DisplayOrder { get; set; }
        public string LinkTinTuc { get; set; }

        public string SEOTitle { get; set; }
        public string SEODescription { get; set; }
        public string SEOKeyword { get; set; }

        public bool? IsShow { get; set; }
        public bool? IsHot { get; set; }

        public string LanguageId { get; set; }
        public bool? IsDeleted { get; set; }
        public int? Sort { get; set; }
        public string PictureUrl { get; set; }
    }
}
