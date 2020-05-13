using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCore
{
    public class LatestOrders
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public int   items { get; set; }
        public decimal Total { get; set; }
    }
    public class LatestReviews
    {
        public int Id { get; set; }
        public string Product { get; set; }
        public string Customer { get; set; }
        public int Rate { get; set; }
        public string Review { get; set; }
    }
}
