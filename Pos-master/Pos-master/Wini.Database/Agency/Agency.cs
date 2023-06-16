using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Wini.Database
{
    [Table("DN_Agency")]
    public class Agency
    {
        [Key]
        public int Id { get; set; }
        public string?  Name { get; set; }
        [NotMapped]
        public string? UserName { get; set; }
        public string? Latitute { get; set; }
        public string? Longitude { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Department { get; set; }
        public string? Address { get; set; }
        public string? Company { get; set; }
        public string? MST { get; set; }
        public string? STK { get; set; }
        public string? FullnameBank { get; set; }
        public string? Branchname { get; set; }
        public string? BankName { get; set; }
        public decimal? CreateDate { get; set; }
        public decimal? DateStart { get; set; }
        public decimal? DateEnd { get; set; }
        public decimal? DateLock { get; set; }
        public bool? IsLock { get; set; }
        public bool? IsOut { get; set; }
        public bool? IsShow { get; set; }
        public bool? IsDelete { get; set; }
        public string?  Code { get; set; }
        public string?  IPTimekeep { get; set; }
        public int? Port { get; set; }
        public int? GroupID { get; set; }
        public bool? IsModule { get; set; }
        public bool? IsWholeSale { get; set; }
        public int? AgencyLevelId { get; set; }
        public string?  Coordinates { get; set; }
        public decimal? WalletValue { get; set; }
        public decimal? CashOut { get; set; }
        public int? MarketID { get; set; }
        public bool? IsFdi { get; set; }
        public int? ParentID { get; set; }
        public string?  ListID { get; set; }
        public int? Level { get; set; }
        public bool? IsActive { get; set; }
        public string?  TokenDevice { get; set; }
        public string?  AvatarUrl { get; set; }
        public string?  ImageTimeline { get; set; }
        public bool? IsVerify { get; set; }
        public bool? IsBank { get; set; }
        public string?  PassWord { get; set; }
        public string?  PasswordSalt { get; set; }
        public bool? IsEnablePass2 { get; set; }
        public string?  Password2 { get; set; }
        public string?  PasswordSalt2 { get; set; }
        public Guid? KeyOtp { get; set; }
        public int? AgencyTypeId { get; set; }

        [ForeignKey("AgencyTypeId")]
        public AgencyType? AgencyType { get; set; }
        public ICollection<Debt> Debts { get; set; }
    }
}
