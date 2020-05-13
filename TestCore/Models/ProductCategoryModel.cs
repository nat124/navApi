using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestCore
{
    public class ProductCategoryModel
    {
        //public ProductCategory()
        //{
        //    this.ProductCategory1 = new List<ProductCategory>();
        //}
        public int Id { get; set; }
        public string Name { get; set; }
        public string SpanishName { get; set; }
        
        public int? ParentId { get; set; }
        public string Icon { get; set; }
        public bool IsActive { get; set; }
        public bool? IsAdult { get; set; }
        public string ActualIcon { get; set; }
        public virtual ProductCategoryModel Parent { get; set; }
        public virtual List<ProductCategoryModel> ProductCategory1 { get; set; }
    }
}