using System;
using System.Collections.Generic;
using System.Text;

namespace Wini.Simple
{
   public class AgencyItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public decimal? CreateDate { get; set; }
        public decimal? DateStart { get; set; }
        public decimal? DateEnd { get; set; }
        public decimal? DateLock { get; set; }
        public bool? IsLock { get; set; }
        public bool? IsOut { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsShow { get; set; }
        public string Code { get; set; }
        public string IpTimekeep { get; set; }
        public string UserName { get; set; }
        public string Pass { get; set; }
        public int? Port { get; set; }
        public int? GroupId { get; set; }
        public int? MarketId { get; set; }
        public int? AreaId { get; set; }
        public decimal? PriceReceive { get; set; }
        public decimal? PriceReward { get; set; }
        public decimal? Total { get; set; }
        public decimal? TotalPay { get; set; }
        public decimal? TotalDisCount { get; set; }
        public decimal? TotalDeposit { get; set; }
        public decimal? Percent { get; set; }
        public string Latitute { get; set; }
        public string Longitude { get; set; }
        public bool? IsFdi { get; set; }
        public decimal? WalletValue { get; set; }
        public decimal? Cashout { get; set; }
        public string Token { get; set; }
        public int? AgencyLevelId { get; set; }
        public string Bankname { get; set; }
        public string Branhname { get; set; }
        public string FullnameBank { get; set; }
        public string Stk { get; set; }
        public IEnumerable<string> LstImage { get; set; }
        public Guid? KeyOtp { get; set; }
        public string TypeName { get; set; }
    }
}
