using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wini.Database
{

    [Table("DN_Roles")]

    public class Roles
    {
        public Roles()
        {

        }

        [Column("ID")]
        public int Id { get; set; }
        public int? AgencyID { get; set; }

        public string RoleName { get; set; }

        public bool IsDeleted { get; set; }

        public string LoweredRoleName { get; set; }public string Description { get; set; }
        public ICollection<UserInRole> userInRoles { get; set; }
    }

}
