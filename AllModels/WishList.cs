using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Models
{
    public class WishList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int ProductVariantDetailId { get; set; }
        public string IpAddress { get; set; }
        public bool IsActive { get; set; }
        public virtual ProductVariantDetail ProductVariantDetail { get; set; }
    }
}
