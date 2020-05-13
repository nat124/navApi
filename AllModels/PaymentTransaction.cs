using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Models
{
    public class PaymentTransaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string intent { get; set; }
        public string orderID { get; set; }
        public string payerID { get; set; }
        public string paymentID { get; set; }
        public string ReturnId { get; set; }
        public string paymentToken { get; set; }
        public decimal? TransactionAmount { get; set; }
        public string PaymentMethod { get; set; }
        public int FeesAmount { get; set; }
        public string StatusDetail { get; set; }

        public int? CheckoutId { get; set; }

        public int? PaymentTypeId { get; set; }
        public DateTime? Date { get; set; }
        public virtual Checkout Checkout { get; set; }
        // public virtual User User { get; set; }
    }

    public class VendorTransaction
    {
        public int Id { get; set; }
        public string TransactionNumber { get; set; }
        public int VendorId { get; set; }
        public int? PaymentTransactionId { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime TransactionDate { get; set; }
        public bool IsPaidByPistis { get; set; }
        public bool IsActive { get; set; }

        public virtual User Vendor { get; set; }
    }

    public class VendorTransactionSummary
    {
        public int Id { get; set; }
        public int VendorId { get; set; }
      //  public int? VendorTransactionId { get; set; }
        public decimal DueAmount { get; set; }
        public DateTime ModifyOn { get; set; }
        public bool IsActive { get; set; }
        public virtual User Vendor { get; set; }

    }

    public class PaymentConfiguration
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public decimal Percentage { get; set; }
        public bool IsApplied  { get; set; }
    }
}