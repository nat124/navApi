using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCore
{
    public class ProductCatalogue
    {

        public ProductCatalogue()
        {
            ProductImages = new List<ProductImageModel>();
            VariantModels = new List<VariantModel>();
            FinalVariants = new List<Variants>();
            ProductSpecifications = new List<ProductSpecification>();
            VariantOptions = new List<VariantOption>();
            ColorVariantOptions = new List<VariantOption>();
        }
        //product
        public int VariantDetailId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsAdult { get; set; }
        public int ProductCategoryId { get; set; }
        
        public int UnitId { get; set; }
        public int InStock { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int Discount { get; set; }
        public decimal PriceAfterDiscount { get; set; }
        public string Description { get; set; }
        public string ProductTags { get; set; }
        public string Barcode { get; set; }
        public bool IsActive { get; set; }
        public int? Rating { get; set; }
        public bool ShipmentVendor { get; set; }
        public int ShipmentTime { get; set; }
        public decimal ShipmentCost { get; set; }
                                           //for list page

        public string ProductCategoryName { get; set; }
        public string Image { get; set; }
        public string Image150 { get; set; }
        public string Image450 { get; set; }

        public bool? ShowMore { get; set; }
        public int Page { get; set; }
        public virtual ICollection<ProductImageModel> ProductImages { get; set; }
        public List<Variants> FinalVariants { get; set; }
        public List<VariantModel> VariantModels { get; set; }
        public List<ProductSpecification> ProductSpecifications { get; set; }
        //checking
        public VariantModel VariantModel { get; set; }
        //paging
        public int Count { get; set; }
        //for variants
        public List<VariantOption> VariantOptions { get; set; }
        public List<VariantOption> ColorVariantOptions { get; set; }

        public List<string> PriceList { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        //review
        public int AvgRate { get; set; }
        public DateTime? ActiveTo { get; set; }

    }
}
