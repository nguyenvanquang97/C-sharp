using System;
using System.Collections.Generic;
using System.Text;

namespace Wini.Simple
{
    public class OrderItem
    {
        public int Id { get; set; }
        public Guid? UserID { get; set; }
        public int? AgencyId { get; set; }
        public int? BedDeskID { get; set; }
        public int? LevelRoomId { get; set; }
        public decimal? TotalPrice { get; set; }
        public int? TotalMinute { get; set; }
        public Guid? UserIdBedDeskID { get; set; }
        public int? AddMinuteID { get; set; }
        public string UserName { get; set; }
        public string UserName1 { get; set; }
        public string UserName2 { get; set; }
        public string CodeUser { get; set; }
        public string BedDeskName { get; set; }
        public string CustomerName { get; set; }
        public string CutomerPhone { get; set; }
        public string CutomerAddress { get; set; }
        public int? CustomerID { get; set; }
        public decimal? DateCreated { get; set; }
        public decimal? StartDate { get; set; }
        public decimal? EndDate { get; set; }
        public string Note { get; set; }
        public string Mobile { get; set; }
        public int? Status { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Deposits { get; set; }
        public bool? IsDelete { get; set; }
        public bool? IsActive { get; set; }
        public decimal? PrizeMoney { get; set; }
        public decimal? Payments { get; set; }
        public decimal? SalePercent { get; set; }
        public decimal? SalePrice { get; set; }

        public string SaleCode { get; set; }
        public string Billnumber { get; set; }
        public string TaxCode { get; set; }
        public string CompanyName { get; set; }
        public IEnumerable<OrderDetailItem> OrderDetailItems { get; set; }
    }
}
