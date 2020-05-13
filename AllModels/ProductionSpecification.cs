using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
   public class ProductionSpecification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ProductId { get; set; }
        public string HeadingName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int VariantDetailId { get; set; }
        public virtual Product Product { get; set; }
    }
}
