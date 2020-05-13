using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCore
{
    public class LoginCheckout
    {
        public LoginCheckout()
        {
            Cart = new List<GetCart>();
        }
        public int? CustomerId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string IpAddress { get; set; }
        public int? SelectedShippingId { get; set; }
        public int? BillingAddressId { get; set; }
        public string ShippingType { get; set; }
        public decimal ShippingPrice { get; set; }
        public string PaymentId { get; set; }
        public virtual Shipping Shipping { get; set; }
        public virtual List<GetCart> Cart { get; set; }

        public PaymentModel CardData { get; set; }
    }
    public class RegisterCheckout
    {
        public RegisterCheckout()
        {
            Cart = new List<GetCart>();
        }
        public string IpAddress { get; set; }
        public int? SelectedShippingId { get; set; }
        public virtual User User { get; set; }
        public virtual Shipping Shipping { get; set; }
        public virtual List<GetCart> Cart { get; set; }
    }
}
