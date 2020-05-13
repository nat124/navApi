using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Models
{
  public  class Shipping
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string IpAddress { get; set; }
        public string  Address { get; set; }
        public string AlternatePhoneNo { get; set; }

        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string Pincode { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public string LandMark { get; set; }
        public int CountryId { get; set; }
        public string AddressType { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
        public string Email { get; set; }

        public virtual string StateName { get; set; }

        public string Street { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string OutsideNumber { get; set; }
        public string InteriorNumber { get; set; }
        public string Colony { get; set; }
        public virtual Country Country { get; set; }
        public virtual State State { get; set; }
      
    }
}
