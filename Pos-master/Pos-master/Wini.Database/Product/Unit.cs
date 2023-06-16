using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wini.Database
{

    [Table("DN_Unit")]
    public class Unit
    {
        public Unit()
        {

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsShow { get; set; }
        public bool? IsDeleted { get; set; }

    }

}
