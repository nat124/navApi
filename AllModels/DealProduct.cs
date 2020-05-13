using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class DealProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int DealId { get; set; }
        public int ProductId { get; set; }
        public int ProductVariantId { get; set; }
        public virtual Deal Deal { get; set; }

    }
}
