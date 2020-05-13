using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCore.Controllers;

namespace TestCore
{
    public class ProductDetail
    {
        public int ProductId { get; set; }
        public int ProductVariantDetailId { get; set; }
        public string Name { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal PriceAfterDiscount { get; set; }
        public int Discount { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public string Description { get; set; }
        public int InStock { get; set; }
        public bool ShipmentVendor { get; set; }
        public int Commission { get; set; }
        public DateTime? ActiveTo { get; set; }
    }
    public class ProductImages
    {
        public int ProductVariantDetailId { get; set; }
        public string Image150 { get; set; }
        public string Image450 { get; set; }
        public string LandingImage { get; set; }
    }

    public class productReview
    {
        public int Onestar { get; set; }
        public int Twostar { get; set; }
        public int Threestar { get; set; }
        public int Fourstar { get; set; }
        public int Fivestar { get; set; }
        public int Onestarper { get; set; }
        public int Twostarper { get; set; }
        public int Threestarper { get; set; }
        public int Fourstarper { get; set; }
        public int Fivestarper { get; set; }
        public float RatingAvg { get; set; }
        public int? Rating { get; set; }
        public int ReviewCount { get; set; }
        public int RatingCount { get; set; }
        public List<UserRating> UserRatings { get; set; }
    }
    public class ProductVariantModel
    {
        public ProductVariantModel()
        {
            Variant = new List<VariantsModel>();
            VariantOption = new HashSet<VariantOption>();
            ProductVariantOption = new List<ProductVariantOption>();
        }
        public List<VariantsModel> Variant { get; set; }
        public HashSet<VariantOption> VariantOption { get; set; }
        public List<ProductVariantOption> ProductVariantOption { get; set; }
    }
   
}
