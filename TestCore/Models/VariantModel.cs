using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestCore
{
    //public class VariantModel
    //{
        
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public bool IsActive { get; set; }

    //    //public virtual ICollection<VariantOption> VariantOptions { get; set; }
    //    //public virtual ICollection<CategoryVariant> CategoryVariants { get; set; }
    //}
    public class VariantOptionModel
    {
        public int Id { get; set; }
        public int VariantId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string varientName { get; set; }

        public virtual VariantsModel Variant { get; set; }
        //public virtual ICollection<ProductVariantOption> ProductVariantOptions { get; set; }
    }
}