using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class Return
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ReturnNumber { get; set; }
        public string Description { get; set; }
        public DateTime ReturnDate { get; set; }
        public int? CheckoutItemId { get; set; }
        public int productVariantDetailId { get; set; }
        public int UnitId { get; set; }
        public int Quantity { get; set; }
        public decimal SellingPrice { get; set; }
        public int Discount { get; set; }
        public decimal Amount { get; set; }
        public int VendorId { get; set; }
        public int ReturnStatusId { get; set; }
        public bool IsPaid { get; set; }
        public bool IsActive { get; set; }
        
    }
    public class ReturnImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ReturnId { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
    }
    public class ReturnStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
