using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Models
{
    public class CompareProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? ProductVariantDetailId { get; set; }
        public bool IsActive { get; set; }
        public string IpAddress { get; set; }
        public int? UserId { get; set; }
        public virtual ProductVariantDetail ProductVariantDetail { get; set; }
        public virtual User User { get; set; }
    }
}
