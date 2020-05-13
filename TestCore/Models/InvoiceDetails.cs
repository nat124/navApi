using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCore
{
    public class InvoiceDetails
    {
        
            public string OrderNo { get; set; }
            public string PurchasedOn { get; set; }
        public string DeliveryDate { get; set; }
        public Nullable<Decimal> BasePrice { get; set; }
        public Nullable<Decimal> ShippingPrice { get; set; }
        public string billtoName { get; set; }
        public string billAddress { get; set; }
        public string billStreets { get; set; }
        public string billCountry { get; set; }
    public string billCity { get; set; }
        public string billState { get; set; }
        public string billPin { get; set; }
        public string billPhoneNo { get; set; }
        public string billColony { get; set; }
        public string billLandmark { get; set; }

        public string ShiptoName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipStreets { get; set; }
        public string ShipColony { get; set; }
        public string ShipLandmark { get; set; }
        public string ShipCountry { get; set; }
        public string ShipCity { get; set; }
        public string ShipState { get; set; }
        public string ShipPin { get; set; }

        public string ShipPhoneNo { get; set; }

        public decimal PurchasePrice { get; set; }
            public string OrderStatus { get; set; }
            public string ShippedBy { get; set; }
            public string PaymentMethod { get; set; }
            public int? Quantity { get; set; }
            public int? VenderId { get; set; }
            public string VenderName { get; set; }
            public string TrackingNumber { get; set; }
            public string UserEmailId { get; set; }
        public List<CheckoutItem> items { get; set; }

    }
}
