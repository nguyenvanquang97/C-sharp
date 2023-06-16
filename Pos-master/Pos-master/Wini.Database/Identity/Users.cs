using System.ComponentModel.DataAnnotations.Schema;

namespace Wini.Database
{

    [Table("DN_Users")]
    public class Users
    {
        [Column("UserId")]
        public Guid Id { get; set; }

        public string? UserName { get; set; }
        public string? PassWord { get; set; }
        public string? LoweredUserName { get; set; }
        public decimal? BirthDay { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsLockedOut { get; set; }
        public bool? IsApproved { get; set; }
        public decimal? CreateDate { get; set; }
        public int? AgencyId { get; set; }
        [ForeignKey("AgencyId")]
        public Agency? Agency { get; set; }
        public ICollection<UserModule> UserModules { get; set; }




    }

}
