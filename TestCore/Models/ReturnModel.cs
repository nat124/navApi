using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCore.Controllers;

namespace TestCore
{
    public class ReturnModel
    {
        public string description { get; set; }
        public List<string> images { get; set; }
        public product1 product { get; set; }
        public int CheckoutItemId { get; set; }
        public int Quantity { get; set; }

    }

}
