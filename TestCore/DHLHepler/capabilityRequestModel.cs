using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCore.DHLHepler
{
    public class capabilityRequestModel
    {
        public string CountryCode { get; set; }
        public string Postalcode { get; set; }
        public int variantDetailId { get; set; }
        public int productId { get; set; }
        public int quantity { get; set; }
        //public virtual ProductModel ProductDetail { get; set; }
    }


    public class trackingRequestModel
    {
        public string CountryCode { get; set; }
        public int Postalcode { get; set; }
        public string trackingNumber { get; set; }
        
        //public virtual ProductModel ProductDetail { get; set; }
    }
}
