using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestCore
{
    public class CategoryVariantModel
    {
        public int Id { get; set; }
        public int VariantId { get; set; }
        public int ProductCategoryId { get; set; }
        public bool IsActive { get; set; }
        public bool IsSearchOption { get; set; }
        public bool IsMain { get; set; }
        public bool MainSelected { get; set; }

        public virtual CategoriesVariantModel VariantData { get; set; }
        public virtual ProductCategoryModel ProductCategory { get; set; }
        //public virtual ICollection<ProductVariantOption> ProductVariantOptions { get; set; }
    }
}