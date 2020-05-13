using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Variant
    {
        public Variant()
        {
            VariantOptions=new HashSet<VariantOption>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string   Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsMain { get; set; }
        public virtual ICollection<VariantOption> VariantOptions { get; set; }
        public virtual ICollection<CategoryVariant> CategoryVariants { get; set; }
    }

    public class VariantOption
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int VariantId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public virtual Variant Variant { get; set; }
        public virtual ICollection<ProductVariantOption> ProductVariantOptions { get; set; }
    }
    
}
