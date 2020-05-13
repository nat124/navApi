using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class ProductCategoryCommission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProductCategoryId { get; set; }
        public int Commission { get; set; }
        public bool IsActive { get; set; }
        [NotMapped]
        public string CategoryName { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
    }
}
