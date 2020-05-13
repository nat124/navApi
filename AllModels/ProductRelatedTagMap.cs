using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Models
{
    public class ProductRelatedTagMap
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int RelatedTagId { get; set; }
        public virtual RelatedTag RelatedTag { get; set; }
        public virtual Product Product { get; set; }

    }
}
