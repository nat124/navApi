using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CategoryVariant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int VariantId { get; set; }
        public int ProductCategoryId { get; set; }
        public bool IsActive { get; set; }
        public bool  IsSearchOption { get; set; }
        public bool IsMain { get; set; }
        public int ControlTypeId { get; set; }


        public virtual Variant Variant { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ICollection<ProductVariantOption> ProductVariantOptions { get; set; }
        public virtual ICollection<ControlType> ControlTypes { get; set; }


    }
}
