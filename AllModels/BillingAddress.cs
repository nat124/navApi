using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class BillingAddress
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Nullable<int> ShippingId { get; set; }
        public int UserId { get; set; }
        public string IpAddress { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string Pincode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string LandMark { get; set; }
        public string Street { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string OutsideNumber { get; set; }
        public string InteriorNumber { get; set; }
        public string Colony { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }

        //public virtual Country Country { get; set; }
    }
}