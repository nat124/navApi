using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestCore
{
    public class CheckOut
    {
        public CheckOut()
        {
            CheckOuts = new List<CheckOut>();
            VariantOptions = new List<TestCore.VariantOptionModel>();
        }
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public string OrderStatus { get; set; }
        public string SellerName { get; set; }
        public int? VendorId { get; set; }
        public int? UserId { get; set; }
        public string IpAddress { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal? AdditionalCost { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsConvertToCheckout { get; set; }
        public bool IsActive { get; set; }
        //From product
        public string Image { get; set; }
        public string Name { get; set; }
        public string Vendor { get; set; }

        //cartitem
        public decimal SellingPrice { get; set; }
        public decimal PriceAfterDiscount { get; set; }
        public int Discount { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public int ProductVariantDetailId { get; set; }
        public int checkoutItemId { get; set; }
        public int UnitId { get; set; }
        public int ProductId { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public bool IsReturned { get; set; }

        public List<TestCore.VariantOptionModel> VariantOptions { get; set; }

        public virtual User User { get; set; }
        public virtual List<CheckOut> CheckOuts { get; set; }
    }
}
