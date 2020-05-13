using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class ShippingConfig
    {
        public int Id { get; set; }
        public Decimal MaxShipPrice { get; set; }
        public Decimal MaxOrderPriceForFreeShip { get; set; }
        public int MyShareOnShipCharge { get; set; }
        public bool IsActive { get; set; }
    }
}
