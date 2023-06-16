using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wini.Database
{

    [Table("Gallery_Picture")]
    public class Picture
    {
        public Picture()
        {

        }
        public int Id { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<int> Type { get; set; }
        public string? Name { get; set; }
        public string Url { get; set; }
        public string? Description { get; set; }
        public Nullable<bool> IsShow { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> SourceId { get; set; }
        public string? Folder { get; set; }
        public string? CreateBy { get; set; }
        public string? UpdateBy { get; set; }
        public string? LanguageId { get; set; }
        public Nullable<decimal> DateCreated { get; set; }

    }

}
