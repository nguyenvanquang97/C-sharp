using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wini.SaleMultipleChannel.Model.Order
{
    public class OrderEcom
    {
      
        public string VoucherPlatform { get; set; }
    
        public string Voucher { get; set; }
    
        public string WarehouseCode { get; set; }

        public string OrderNumber { get; set; }

        public string VoucherSeller { get; set; }
   
        public DateTime CreatedAt { get; set; }

        public string VoucherCode { get; set; }

        public string GiftOption { get; set; }

        public string ShippingFeeDiscountPlatform { get; set; }

        public string CustomerLastName { get; set; }

        public string PromisedShippingTimes { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string Price { get; set; }

        public string NationalRegistrationNumber { get; set; }

        public string ShippingFeeOriginal { get; set; }
   
        public string PaymentMethod { get; set; }

        public string AddressUpdatedAt { get; set; }
      
        public string CustomerFirstName { get; set; }
       
        public string ShippingFeeDiscountSeller { get; set; }

        public string ShippingFee { get; set; }

        public string BranchNumber { get; set; }

        public string TaxCode { get; set; }

        public string ItemsCount { get; set; }

        public string DeliveryInfo { get; set; }
   
        public List<object> Statuses { get; set; }
     
        public AddressBilling AddressBilling { get; set; }

        public string ExtraAttributes { get; set; }

        public string Order_id { get; set; }

        public string Remarks { get; set; }
 
        public string GiftMessage { get; set; }
    
        public AddressShipping AddressShipping { get; set; }
    }
}
