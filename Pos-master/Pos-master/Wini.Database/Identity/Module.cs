using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wini.Database
{

    [Table("DN_Module")]
    public class Module
    {
        public Module()
        {

        }

        [Column("ID")]
        public int Id { get; set; }

        [Column("ParentID")]
        public int? ParentId { get; set; }

        public string? NameModule { get; set; }
        public string? Link { get; set; }
        public bool? IsDelete { get; set; }


        [ForeignKey("ParentId")]
        public virtual ICollection<Module> Children { get; set; }

        public ICollection<UserModule> UserModules { get; set; }


    }

}
