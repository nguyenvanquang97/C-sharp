using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wini.Database
{

    [Table("Product_Size")]
    public class Size
    {
        public Size()
        {

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsShow { get; set; }
        public bool? IsDeleted { get; set; }
        public int? UnitId { get; set; }
        public Unit Unit { get; set; }


    }

}
