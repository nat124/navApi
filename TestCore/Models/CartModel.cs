using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCore
{
    public class GetCart
    {
        public GetCart()
        {
            CartItems = new List<CartItem>();
            VariantOptions = new List<TestCore.VariantOptionModel>();
        }
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string OrderNumber { get; set; }
        public int? VendorId { get; set; }
        public string VendorName { get; set; }
        public int? DealQuantityPerUser { get; set; }
        public int? UserId { get; set; }
        public string ShippingType { get; set; }
        public decimal ShippingPrice { get; set; }
        public string IpAddress { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal? AdditionalCost { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsConvertToCheckout { get; set; }
        public bool IsActive { get; set; }
        //From product
        public string Image { get; set; }
        public string Image150 { get; set; }

        public string Name { get; set; }
        
        public bool ShipmentVendor { get; set; }
        public int ShipmentTime { get; set; }
        public decimal ShipmentCost { get; set; }
        public string Vendor { get; set; }

        //cartitem
        public decimal SellingPrice { get; set; }
        public decimal PriceAfterDiscount { get; set; }
        public decimal DealPriceAfterDiscount { get; set; }
        public int Discount { get; set; }
        public int? DealDiscount { get; set; }
        public int? DealQty { get; set; }

        public int Commission { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public int ProductVariantDetailId { get; set; }
        public int CartItemId { get; set; }
        public int UnitId { get; set; }
        public DateTime DeliveryDate { get; set; }

        public int InStock { get; set; }

        public List<TestCore.VariantOptionModel> VariantOptions { get; set; }

        public virtual User User { get; set; }
        public virtual List<CartItem> CartItems { get; set; }
    }
    public class CartStock
    {
        public int Id { get; set; }
        public int CartItemId { get; set; }
        public int MaxStock { get; set; }
        public bool IsStockAvailable { get; set; }
    }
}
