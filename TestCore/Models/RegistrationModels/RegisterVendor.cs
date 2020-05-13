using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestCore
{
    public class RegisterVendor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Image { get; set; }
        public Nullable<DateTime> DOB { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Address { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public int? CompanyId { get; set; }
        public int RoleId { get; set; }
        public int? LanguageId { get; set; }
        public int? GenderId { get; set; }
        public bool IsVerified { get; set; }
        public bool IsActive { get; set; }
        public string FacebookId { get; set; }
        public string TwitterId { get; set; }
        public string Password { get; set; }
        public int? ReturnCode { get; set; }
        public string ReturnMessage { get; set; }
        public string Company { get; set; }
        public string Logo { get; set; }
        public string IdProof { get; set; }
        public string VendorId { get; set; }
        public string RFC { get; set; }

        //public virtual Role Role { get; set; }
        //public virtual Language Language { get; set; }
        //public virtual Gender Gender { get; set; }
        //public virtual Country Country { get; set; }
        //public virtual State State { get; set; }
        //public virtual Company Company { get; set; }
    }
}