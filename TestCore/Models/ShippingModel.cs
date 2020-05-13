using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCore
{
    public class ShippingModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string IpAddress { get; set; }
        public string Address { get; set; }

        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string AlternatePhoneNo { get; set; }
        public string Pincode { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public string LandMark { get; set; }
        public int CountryId { get; set; }
        public string AddressType { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
        public virtual Country Country { get; set; }
        //public virtual State State { get; set; }

        public string State { get; set; }
        public string Street { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string OutsideNumber { get; set; }
        public string InteriorNumber { get; set; }
        public string Colony { get; set; }

        //forcheckout page
        public string CountryName { get; set; }
        public string StateName { get; set; }
    }

    public class ShippingModel1
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string IpAddress { get; set; }

        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string AlternatePhoneNo { get; set; }
        public string Pincode { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public string LandMark { get; set; }
        public int CountryId { get; set; }
        public string AddressType { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
        public virtual Country Country { get; set; }
        //public virtual State State { get; set; }

        public string State { get; set; }
        public string Street { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string OutsideNumber { get; set; }
        public string InteriorNumber { get; set; }
        public string Colony { get; set; }

        //forcheckout page
        public string CountryName { get; set; }
        public string StateName { get; set; }
    }

    public class ShippingBillAddressModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string IpAddress { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string Pincode { get; set; }
        public string City { get; set; }
        public string LandMark { get; set; }
        public int CountryId { get; set; }
        public string Street { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string OutsideNumber { get; set; }
        public string InteriorNumber { get; set; }
        public string Colony { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }

        //forcheckout page
        public string CountryName { get; set; }
        public string State { get; set; }
    }
}