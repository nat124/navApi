using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Models
{
    public class ProductTag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int TagId { get; set; }
        public bool IsApproved { get; set; }
        public bool IsActive { get; set; }
        public virtual Tag Tag { get; set; }
        public virtual Product Product { get; set; }

    }
}
