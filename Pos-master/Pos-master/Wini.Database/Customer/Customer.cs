using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wini.Database
{
    [Table("Customer")]
    public class Customer
    {
        public int ID { get; set; }
        public int? DistrictID { get; set; }
        public int? CityID { get; set; }
        public int? AgencyId { get; set; }
        public int? PictureID { get; set; }
        public string PassWord { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public bool? Gender { get; set; }
        public decimal? Birthday { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PeoplesIdentity { get; set; }
        public decimal? DateCreated { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
        
        public string Mobile { get; set; }
        public int? Type { get; set; }
        
        public bool IsPrestige { get; set; }
        public string AvatarUrl { get; set; }
       
        public string STK { get; set; }
        public string FullnameBank { get; set; }
        public string Branchname { get; set; }
        public string BankName { get; set; }
        public int? BankId { get; set; }
        
    }
}
