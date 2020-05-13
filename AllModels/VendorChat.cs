using System;
using System.Collections.Generic;
using System.Text;

namespace AllModels
{
   public  class VendorChat
    {
        public int Id { get; set; }
        public int VendorId { get; set; }
        public int CustomerId { get; set; }
        public int ProductVariantDetailId { get; set; }
        public string IpAddress { get; set; }
        public Boolean IsArchieved { get; set; }
        public Boolean IsActive { get; set; }
    }

    public class VendorChatMsg
    {
        public VendorChatMsg()
        {
            VendorChat = new VendorChat();
        }
        public int Id { get; set; }
        public int VendorChatId { get; set; }
        public string CustomerMsg { get; set; }
        public string VendorMsg { get; set; }
        public bool IsCustomerRead { get; set; }
        public bool IsVendorRead { get; set; }
        public DateTime DateTime { get; set; }
        public virtual VendorChat  VendorChat { get; set; }
    }
}
