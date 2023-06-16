using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wini.Database
{

    [Table("DN_User_ModuleActive")]
    public class UserModuleActive
    {
        public UserModuleActive()
        {

        }

        [Column("ID")]
        public int Id { get; set; }

        public Guid UserId { get; set; }
        public int ModuleId { get; set; }

        public int ActiveId { get; set; }
        public int Check { get; set; }
        public bool Active { get; set; }

    }

}
