using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class HomeCategoryProduct
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int HomeCategoryId { get; set; }
        public int ProductId { get; set; }
        public bool IsActive { get; set; }
        public virtual HomeCategory HomeCategory { get; set; }
        public virtual Product Product { get; set; }

    }
}