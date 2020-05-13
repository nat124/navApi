using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductCategoryId { get; set; }
        public int UnitId { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int Discount { get; set; }
        public decimal PriceAfterdiscount { get; set; }
        public string Description { get; set; }
        public string ProductTags { get; set; }
        //public string ProductRelatedTags { get; set; }
        public string Barcode { get; set; }
        public bool IsActive { get; set; }
        public int VendorId { get; set; }
        public bool IsEnable { get; set; }
        public bool? ShipmentVendor { get; set; }
        public int? ShipmentTime { get; set; }
        public decimal? ShipmentCost { get; set; }
        public virtual ICollection<ProductTag> ProductTag { get; set; }
        public virtual ICollection<HomeCategoryProduct> HomeCategoryProduct { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<ProductionSpecification> ProductionSpecifications { get; set; }
        public virtual ICollection<ProductVariantDetail> ProductVariantDetails { get; set; }
        public virtual ICollection<FeatureProduct> FeatureProducts { get; set; }
        public virtual ICollection<RatingReview> RatingReviews { get; set; }
    }
    public class ProductVariantOption
    {
        public int Id { get; set; }
        public int ProductVariantDetailId { get; set; }
        public int CategoryVariantId { get; set; }
        public int VariantOptionId { get; set; }
        public bool IsActive { get; set; }

        public virtual ProductVariantDetail ProductVariantDetail { get; set; }
        public virtual CategoryVariant CategoryVariant { get; set; }
        public virtual VariantOption VariantOption { get; set; }

        public virtual ICollection<StockTransaction> StockTransactions { get; set; }




    }


    public class ProductImage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string ImagePath150x150 { get; set; }
        public string ImagePath450x450 { get; set; }
        public int ProductId { get; set; }
        public int ProductVariantDetailId { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }

        public virtual Product Product { get; set; }
        public virtual ProductVariantDetail ProductVariantDetail { get; set; }
    }
    public class ProductVariantDetail
    {
        public ProductVariantDetail()
        {
            CartItems = new List<CartItem>();
            ProductImages = new List<ProductImage>();
            WishLists = new List<WishList>();
            CompareProducts = new List<CompareProduct>();
            ColorVariantOptions = new List<VariantOption>();
        }
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int InStock { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
        public decimal CostPrice { get; set; }
        public int Discount { get; set; }
        [NotMapped]
        public int Commission { get; set; }
        [NotMapped]
        public DateTime? ActiveTo { get; set; }
        public decimal PriceAfterdiscount { get; set; }
        public decimal Weight { get; set; }
        public decimal? Lenght { get; set; }
        public decimal? Width { get; set; }
        public decimal? Height { get; set; }


        public string ProductSKU { get; set; }

        public virtual Product Product { get; set; }
        [NotMapped]
        public virtual List<VariantOption> ColorVariantOptions { get; set; }
        public virtual List<ProductVariantOption> ProductVariantOptions { get; set; }
        public virtual List<ProductImage> ProductImages { get; set; }
        public virtual List<CartItem> CartItems { get; set; }
        public virtual ICollection<WishList> WishLists { get; set; }
        public virtual ICollection<CompareProduct> CompareProducts { get; set; }

    }
}