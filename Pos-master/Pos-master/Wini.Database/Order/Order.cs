using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Wini.Database
{
    [Table("Shop_Order")]
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public Guid? UserId { get; set; }
        public Guid? UserCreate { get; set; }
        public Guid? UserIdBedDeskID { get; set; }
        public int? AgencyId { get; set; }
        public int? ContactOrderID { get; set; }
        public int? TotalMinute { get; set; }
        public int? CustomerID { get; set; }
        public decimal? DateCreated { get; set; }
        public decimal? StartDate { get; set; }
        public decimal? EndDate { get; set; }
        public string Note { get; set; }
        public decimal? Payments { get; set; }
        public decimal? Discount { get; set; }
        public decimal? PrizeMoney { get; set; }
        public decimal? Deposits { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? PriceReceipt { get; set; }
        public int? Status { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string Company { get; set; }
        public string CodeCompany { get; set; }
        public string Mobile { get; set; }
        public string AddressCompany { get; set; }
        public bool? IsEarly { get; set; }
       
        public string Billnumber { get; set; }
        public string TaxCode { get; set; }
        public string CompanyName { get; set; }
        public decimal? SalePercent { get; set; }
        public string SaleCode { get; set; }
        public bool? Isinvoice { get; set; }
        public decimal? Total { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal? TotalSaleSP { get; set; }
        public string OrderCode { get; set; }
        public decimal? TotalPriceTranfer { get; set; }
        public int? Type { get; set; }
        public decimal? ReceiveDate { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public string Code { get; set; }
        public decimal? FeeShip { get; set; }
        public int? StatusPayment { get; set; }
        public string Coupon { get; set; }
        public int? ShopID { get; set; }
        public decimal? CouponPrice { get; set; }
        public int? PaymentMethodId { get; set; }
        public int? CustomerAddressID { get; set; }
        public int? Check { get; set; }
        public decimal? DateUpdateStatus { get; set; }
        public int? AdminConfirm { get; set; }
        public decimal? DateProcessReceive { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
