using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        public string City { get; set; }
        public string PostalCode { get; set; }
        public int? CompanyId { get; set; }
        public int RoleId { get; set; }
        public int? LanguageId { get; set; }
        public int? GenderId { get; set; }
        public bool IsVerified { get; set; }
        public bool IsActive { get; set; }
        public DateTime? DateTime { get; set; }
        public string Otp { get; set; }
        public string FacebookId { get; set; }
        public string TwitterId { get; set; }
        public string VendorId { get; set; }
        public string RFC { get; set; }
        [NotMapped]
        public string Password { get; set; }
        [NotMapped]
        public int ReturnCode { get; set; }
        [NotMapped]
        public string ReturnMessage { get; set; }

        public virtual Role Role { get; set; }
        public virtual Language Language { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual Country Country { get; set; }
     //   public virtual Testimonial Testimonial { get; set; }
        public virtual State State { get; set; }
        public virtual Company Company { get; set; }

        //public virtual List<Cart> VendorCarts { get; set; }
        public virtual List<Cart> Carts { get; set; }
    //    public virtual List<PaymentTransaction> PaymentTransaction { get; set; }
        public virtual List<CartItem> CartItems { get; set; }
        public virtual ICollection<CompareProduct> CompareProducts { get; set; }
    }
}
