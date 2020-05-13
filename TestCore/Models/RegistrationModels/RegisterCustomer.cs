using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestCore
{
    public class RegisterCustomer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Image { get; set; }
        public Nullable<DateTime> DOB { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public int RoleId { get; set; }
        public int? LanguageId { get; set; }
        public int? GenderId { get; set; }
        public bool IsVerified { get; set; }
        public bool IsActive { get; set; }

        public string Password { get; set; }

        public int ReturnCode { get; set; }

        public string ReturnMessage { get; set; }

        //public virtual RoleModel Role { get; set; }
        //public virtual LanguageModel Language { get; set; }
        //public virtual GenderModel Gender { get; set; }
    }
}