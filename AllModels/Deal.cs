using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class Deal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public DateTime ActiveFrom { get; set; }
        public DateTime ActiveTo { get; set; }
        public Decimal Discount { get; set; }
        public string ActiveFromTime { get; set; }
        public string ActiveToTime { get; set; }
        public string Name { get; set; }
        public int DealQty { get; set; }
        public int SoldQty { get; set; }
        public bool IsActive { get; set; }
        public bool IsFeatured { get; set; }
        public string Status { get; set; }
        public int QuantityPerUser { get; set; }
        public int ProductCategoryId { get; set; }
        public List<DealProduct> DealProduct { get; set; }
        public bool IsShow { get; set; }
    }
}
