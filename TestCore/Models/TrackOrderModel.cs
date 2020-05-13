using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCore
{
    public class TrackOrderModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Status { get; set; }
        public string TrackId { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public Checkout OrderDetail { get; set; }
        public Shipping DeliveryAddress { get; set; }
        public List<CheckOut> checkoutItems { get; set; }

    }


    public class TackCompositeModel
    {
        public TrackOrderModel TrackOrder { get; set; }
        public User UserInfo { get; set; }
        public Shipping ShippingInfo { get; set; }
        public BillingAddress BillingInfo { get; set; }
    }

}