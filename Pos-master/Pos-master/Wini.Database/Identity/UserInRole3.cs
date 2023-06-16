using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wini.Database
{

  


    [Table("DN_UsersInRoles")]
    public class UserInRole
    {
        public UserInRole()
        {
            IsDelete = false;
        }

        [Column("ID")]
        public int Id { get; set; }
        public int? AgencyID { get; set; }

        public Guid UserId { get; set; }


        public int? RoleId { get; set; }

        public bool? IsDelete { get; set; }
        public decimal? DateCreated { get; set; }

        [ForeignKey("RoleId")]
        public Roles Product { get; set; }


    }


}
