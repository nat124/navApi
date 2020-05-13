using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCore
{
    public class VendorBalance
    {
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public string   Phone { get; set; }
        public string   Email { get; set; }
        public DateTime LastPaymentDate { get; set; }
        public decimal? AmountDue { get; set; }
        public decimal? Payment { get; set; }
        public string Transno { get; set; }
    }
    public class VendorBalanceDetail
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public int vendorId { get; set; }
        public string isPaid { get; set; }
    }
}
