using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wini.Database
{

    [Table("System_Color")]
    public class Color
    {
        public Color()
        {

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public bool IsShow { get; set; }
        public bool? IsDeleted { get; set; }
        public string LanguageId { get; set; }

    }

}
