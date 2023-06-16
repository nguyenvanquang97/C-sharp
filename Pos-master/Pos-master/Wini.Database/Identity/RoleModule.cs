using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wini.Database
{

    [Table("DN_Role_ModuleActive")]
    public class RoleModuleActive
    {
        public RoleModuleActive()
        {

        }

        [Column("ID")]

        public int Id { get; set; }
        public int ModuleId { get; set; }

        public int RoleId { get; set; }

        
        public int ActiveId { get; set; }
        public bool Check { get; set; }


    }

}
