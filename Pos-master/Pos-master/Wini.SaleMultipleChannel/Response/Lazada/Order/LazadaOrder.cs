using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wini.SaleMultipleChannel.Response.Lazada.Order
{
    public class LazadaOrder
    {
        [JsonProperty("voucher_platform")]
        public string VoucherPlatform { get; set; }
        [JsonProperty("voucher")]
        public string Voucher { get; set; }
        [JsonProperty("warehouse_code")]
        public string WarehouseCode { get; set; }
        [JsonProperty("order_number")]
        public string OrderNumber { get; set; }
        [JsonProperty("voucher_seller")]
        public string VoucherSeller { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("voucher_code")]
        public string VoucherCode { get; set; }
        [JsonProperty("gift_option")]
        public string GiftOption { get; set; }
        [JsonProperty("shipping_fee_discount_platform")]
        public string ShippingFeeDiscountPlatform { get; set; }
        [JsonProperty("customer_last_name")]
        public string CustomerLastName { get; set; }
        [JsonProperty("promised_shipping_times")]
        public string PromisedShippingTimes { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty("price")]
        public string Price { get; set; }
        [JsonProperty("national_registration_number")]
        public string NationalRegistrationNumber { get; set; }
        [JsonProperty("shipping_fee_original")]
        public string ShippingFeeOriginal { get; set; }
        [JsonProperty("payment_method")]
        public string PaymentMethod { get; set; }
        [JsonProperty("address_updated_at")]
        public string AddressUpdatedAt { get; set; }
        [JsonProperty("customer_first_name")]
        public string CustomerFirstName { get; set; }
        [JsonProperty("shipping_fee_discount_seller")]
        public string ShippingFeeDiscountSeller { get; set; }
        [JsonProperty("shipping_fee")]
        public string ShippingFee { get; set; }
        [JsonProperty("branch_number")]
        public string BranchNumber { get; set; }
        [JsonProperty("tax_code")]
        public string TaxCode { get; set; }
        [JsonProperty("items_count")]
        public string ItemsCount { get; set; }
        [JsonProperty("delivery_info")]
        public string DeliveryInfo { get; set; }
        [JsonProperty("statuses")]
        public List<object> Statuses { get; set; }
        [JsonProperty("address_billing")]
        public LazadaAddressBilling AddressBilling { get; set; }
        [JsonProperty("extra_attributes")]
        public string ExtraAttributes { get; set; }
        [JsonProperty("order_id")]
        public string Order_id { get; set; }
        [JsonProperty("remarks")]
        public string Remarks { get; set; }
        [JsonProperty("gift_message")]
        public string GiftMessage { get; set; }
        [JsonProperty("address_shipping")]
        public LazadaAddressShipping AddressShipping { get; set; }
    }

}
