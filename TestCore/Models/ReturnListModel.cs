using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCore
{
    public class ReturnListModel
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string ReturnNumber { get; set; }
        public string Description { get; set; }

        public DateTime ReturnDate { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public string ReturnStatus { get; set; }
        public bool IsPaid { get; set; }

    }
}
