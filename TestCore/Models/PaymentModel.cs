using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCore
{
    public class PaymentModel
    {
        public string token { get; set; }
        public float? totalAmount { get; set; }
        public string email { get; set; }
        public int userid { get; set; }
        public int installments { get; set; }
        public decimal InstallmentRate { get; set; }
        public decimal? InstallmentAmount { get; set; }
        public string IssuerId { get; set; }
        public string PaymentMethodId { get; set; }

    }
}
