using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ProductCategory
    {
        public ProductCategory()
        {
            this.ProductCategory1 = new HashSet<ProductCategory>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string SpanishName { get; set; }
        public int? ParentId { get; set; }
        public string Icon { get; set; }
        //public string Icon150 { get; set; }
        //public string Icon450 { get; set; }

        public bool IsActive { get; set; }
        public bool? IsAdult { get; set; }
        public bool IsShow { get; set; }
        public string ActualIcon { get; set; }

        [ForeignKey("ParentId")]
        public  ProductCategory Parent { get; set; }
        public virtual ICollection<ProductCategory> ProductCategory1 { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<CategoryVariant> CategoryVariants { get; set; }
    }
}
