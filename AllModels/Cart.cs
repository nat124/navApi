using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class Cart
    {
        public Cart()
        {
            CartItems = new List<CartItem>();
        }
        [Key]
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public int? VendorId { get; set; }
        public int? UserId { get; set; }
        public string IpAddress { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal? AdditionalCost { get; set; }
        public bool IsConvertToCheckout { get; set; }
        public bool IsActive { get; set; }

       

        // public virtual User Vendor { get; set; }
        public virtual User User { get; set; }
        public virtual List<CartItem> CartItems { get; set; }
    }

    public class CartItem
    {
        public CartItem()
        {
            
            //ProductVariantDetail = new ProductVariantDetail();
            Unit = new Unit();
            Cart = new Cart();
        }
        
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductVariantDetailId { get; set; }
        public int UnitId { get; set; }
        public int VendorId { get; set; }
        [NotMapped]
        public string VendorName { get; set; }
        public int Quantity { get; set; }
        public bool IsActive { get; set; }
        public bool? ShipmentVendor { get; set; }

        //[ForeignKey("VendorId")]
        public virtual User User { get; set; }
        public virtual ProductVariantDetail ProductVariantDetail { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual Cart Cart { get; set; }

    }

    

    
}
