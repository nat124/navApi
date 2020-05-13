using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
   public class Checkout
    {
        public Checkout()
        {
            CheckoutItems = new List<CheckoutItem>();
        }
        public int Id { get; set; }
        public string ShippingType { get; set; }
        public decimal ShippingPrice { get; set; }
        public string InvoiceNumber { get; set; }
        public int CartId { get; set; }
        public int? UserId { get; set; }
        public string IpAddress { get; set; }
        public DateTime CheckoutDate { get; set; }
        public int LoyalityPointsUsed { get; set; }
        public decimal DiscountForLoyalityPoints { get; set; }
        public decimal? AdditionalCost { get; set; }
        public decimal TotalAmount { get; set; }
        public int PaymentModeId { get; set; }
        public bool IsPaid { get; set; }
        public bool IsActive { get; set; }
        public int ShippingId { get; set; }
        public int BillingAddressId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public virtual User User { get; set; }
        public virtual Cart Cart { get; set; }

        //EMI
        public decimal? PaidAmount { get; set; }
        public DateTime? LastPaymentDate { get; set; }
        public DateTime? NextPaymentDate { get; set; }
        public int TotalInstallments { get; set; }
        public int InstalmentsPaid { get; set; }
        public decimal? InterstRate { get; set; }
        public string PaymentId { get; set; }

        //EMI End
        public virtual PaymentMode PaymentMode { get; set; }
        //public virtual TrackOrder TrackOrder { get; set; }
        public virtual List<CheckoutItem> CheckoutItems { get; set; }
    }

    public class CheckoutItem
    {
        //public CheckoutItem()
        //{
        //    ProductVariantDetail = new ProductVariantDetail();
        //}
        public int Id { get; set; }
        public int CheckoutId { get; set; }
        public int ProductVariantDetailId { get; set; }
        public int UnitId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int Discount { get; set; }
        public int? DealQty { get; set; }
        public int? DealDiscount { get; set; }
        public decimal Amount { get; set; }
        public int OrderStatusId { get; set; }
        public int VendorId { get; set; }
        public int ReturnedQuantity { get; set; }
        public bool IsActive { get; set; }
        public int? Commission { get; set; }
        public virtual Checkout Checkout { get; set; }
        public virtual ProductVariantDetail ProductVariantDetail { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
        public virtual User User { get; set; }
        

    }

    public class PaymentMode
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
    public class OrderStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
