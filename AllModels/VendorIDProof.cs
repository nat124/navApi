using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
   public class VendorIDProof
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Proof { get; set; }
        public bool IsActive { get; set; }

        public virtual User user { get; set; }
    }
}
