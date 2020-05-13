using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
 public   class HomeCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int OrderNumber { get; set; }
        public bool? IsDesktop { get; set; }
        public string Shape { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<HomeCategoryProduct> HomeCategoryProduct { get; set; }

    }
}
