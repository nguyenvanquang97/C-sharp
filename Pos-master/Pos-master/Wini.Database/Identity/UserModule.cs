using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wini.Database
{

    [Table("DN_User_Module")]
    public class UserModule
    {
        public UserModule()
        {

        }

        [Column("ModuleId")]
        public int Id { get; set; }

        public Guid UserId { get; set; }
    }

}
