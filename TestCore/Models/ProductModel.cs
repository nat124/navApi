using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace TestCore
{
    //ModelBinder(BinderType = typeof(AuthorEntityBinder))]
    public class ProductModel
    {
        
        public ProductModel()
        {
             ProductCategory = new ProductCategoryModel();
             ProductImages = new List<ProductImageModel>();
             Unit = new UnitModel();
            //var ProductVariantDetails = new ProductVariantDetailModel();
            ProductVariantDetails = new List<ProductVariantDetailModel>();
             FileUploads = new List<string>();
             VariantImages = new List<VariantImage>();
             VariantModels = new List<VariantModel>();
             FinalVariants = new List<Variants>();
             ProductSpecifications = new List<ProductSpecification>();
             VariantOptions = new List<VariantOption>();
        }
        //product
        public bool? ShipmentVendor { get; set; }
        public int? ShipmentTime { get; set; }
        public decimal? ShipmentCost { get; set; }
        public int VariantDetailId { get; set; }
        public int Id { get; set; }
        public int? MainCategoryId { get; set; }
        public string Name { get; set; }
        public int ProductCategoryId { get; set; }
        public int UnitId { get; set; }
        public decimal CostPrice { get; set; }
       
        public decimal SellingPrice { get; set; }
        public int Discount { get; set; }
        public int Commission { get; set; }
        public decimal PriceAfterDiscount { get; set; }
        public string Description { get; set; }
        public string ProductTags { get; set; }
        public string ProductRelatedTags { get; set; }
        public string Barcode { get; set; }
        public bool IsActive { get; set; }
        public bool IsEnabled { get; set; }
        public int VendorId { get; set; }
        public int? Rating { get; set; }
        public bool IsEnable { get; set; }
        //for list page
        public string Inventory { get; set; }
        public string ProductCategoryName { get; set; }
        public string Image { get; set; }
        public string Image150 { get; set; }
        public string Image450 { get; set; }

        public virtual ProductCategoryModel ProductCategory { get; set; }
        public virtual UnitModel Unit { get; set; }
        public virtual ICollection<ProductImageModel> ProductImages { get; set; }
        public virtual List<ProductVariantDetailModel> ProductVariantDetails { get; set; }
        public List<Variants> FinalVariants { get; set; }
        [BindProperty]
        public List<VariantModel> VariantModels { get; set; }
        [BindProperty]
        public List<VariantImage> VariantImages { get; set; }
        public List<string> FileUploads { get; set; }
        public List<ProductSpecification> ProductSpecifications { get; set; }
        //checking
        public VariantModel VariantModel { get; set; }
        //paging
        public int Count { get; set; }
        //for variants
        public List<VariantOption> VariantOptions { get; set; }
        public List<string> PriceList { get; set; }

    }
    public class Variants
    {
        public string option { get; set; }
        public List<string> optionvalue { get; set; }
        public Boolean isdefault { get; set; }
    }

    public class ProductSpecification
    {
        public string SpecificationHeading { get; set; }
        public string Specification { get; set; }
        public string HeadingName { get; set; }
        public string Description { get; set; }
        public string Heading { get; set; }

    }
    public class VariantModel
    {
        public VariantModel()
        {
            Specification = new List<ProductSpecification>();
        }
        public int Id { get; set; }
        public string variants { get; set; }
        public decimal price { get; set; }
        public int quantity { get; set; }
        public Boolean isDefault { get; set; }
        public decimal CostPrice { get; set; }
        public string ProductSKU1 { get; set; }
        public decimal Weight1 { get; set; }

        public decimal? Lenght1 { get; set; }
        public decimal? Width1 { get; set; }
        public decimal? Height1 { get; set; }
        public int Discount { get; set; }
        public decimal PriceAfterdiscount { get; set; }
        public int ProductVariantDetailId { get; set; }
        public virtual List<ProductSpecification> Specification { get; set; }
    }
    public class VariantImage
    {
        public string variantValue { get; set; }
        public Boolean IsDefaultImage { get; set; }
        public string image { get; set; }
        public List<string> FileUploads { get; set; }
    }






    public class ProductVariantOption
    {
        public int Id { get; set; }
        public int ProductVariantDetailId { get; set; }
        public int CategoryVariantId { get; set; }
        public int VariantOptionId { get; set; }
        public bool IsActive { get; set; }

        public virtual ProductVariantDetailModel ProductVariantDetail { get; set; }
        public virtual CategoryVariantModel CategoryVariant { get; set; }
        public virtual VariantOptionModel VariantOption { get; set; }

        public virtual ICollection<StockTransactionModel> StockTransactions { get; set; }
    }


    public class ProductImageModel
    {
        public int Id { get; set; }
        public int ProductVariantDetailId { get; set; }

        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string ImagePath150 { get; set; }
        public string ImagePath450 { get; set; }
        public int ProductId { get; set; }

        public Boolean IsDefault { get; set; }
        public string Variants { get; set; }
    }
    public class ProductVariantDetailModel
    {
        public ProductVariantDetailModel()
        {
            ProductVariantOptions = new List<ProductVariantOption>();
            ProductImages = new List<ProductImageModel>();
            ProductSpecifications = new List<ProductSpecification>();
        }
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int InStock { get; set; }

        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
        public decimal CostPrice { get; set; }
        public int Discount { get; set; }
        public decimal PriceAfterdiscount { get; set; }
        public string ProductSKU { get; set; }
        public decimal Weight { get; set; }
        public decimal? Lenght { get; set; }
        public decimal? Width { get; set; }
        public decimal? Height { get; set; }
        public string Image { get; set; }
        public List<ProductVariantOption> ProductVariantOptions { get; set; }
        public virtual List<ProductImageModel> ProductImages { get; set; }
        public virtual List<ProductSpecification> ProductSpecifications { get; set; }
    }


    public class CategoriesVariantModel
    {
        public CategoriesVariantModel()
        {
            VariantOptions = new List<CategoriesVariantoprionsModel>();
        }
        public int VariantId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public Boolean IsActive { get; set; }
        public bool IsMain { get; set; }

        public Boolean IsSearchOption { get; set; }
        public List<CategoriesVariantoprionsModel> VariantOptions { get; set; }
        public string AllOptions { get; set; }
        public ProductCategoryModel Category { get; set; }
    }

    public class CategoriesVariantoprionsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int VariantId { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsSearchOption { get; set; }

    }

}