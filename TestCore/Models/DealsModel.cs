using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCore
{
    public class DealsModel
    {
        public DealsModel()
        {
            DealProduct = new List<DealProduct>();
        }

        public int Id { get; set; }
        public string ActiveToTime { get; set; }
        public string ActiveFromTime { get; set; }
        public int CategoryId { get; set; }
        public int ProductCategoryId { get; set; }
        public DateTime ActiveFrom { get; set; }
        public DateTime ActiveTo { get; set; }
        public Decimal Discount { get; set; }
        public int DealQty { get; set; }
        public int SoldQty { get; set; }
        public bool IsActive { get; set; }
        public bool IsFeatured { get; set; }
        public string Status { get; set; }
        public string RegularPrice { get; set; }
        public string Name { get; set; }
        public int QuantityPerUser { get; set; }
        public List<DealProduct> DealProduct { get; set; }

        public virtual ProductVariantDetail VariantData { get; set; }
        public bool IsShow { get; set; }
    }

    public class DealProductModel
    {
        public int Id { get; set; }
        public bool Selected { get; set; }
        public string Name { get; set; }
        public bool ShipmentVendor { get; set; }
        public int ShipmentTime { get; set; }
        public decimal ShipmentCost { get; set; }
        public decimal Price { get; set; }
        public string SKU { get; set; }
        public string Category { get; set; }
        public int CategoryId { get; set; }
        public int ProductId { get; set; }
        public bool AlreadyInDeal { get; set; }

    }
}