using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
   public class VendorDocuments
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Document { get; set; }
        public bool IsActive { get; set; }

        public virtual User user { get; set; }
    }
}
