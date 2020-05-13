using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
   public  class CheckoutLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? CartId { get; set; }
        public int? UserId { get; set; }
        public string IpAddress { get; set; }
        public DateTime? StartTime { get; set; }
        public string Step { get; set; }
        public int? ShippingCharges { get; set; }
        public bool Pass { get; set; }
        public string Error { get; set; }
        public string Action { get; set; }
        public DateTime? Date { get; set; }
    }
}
