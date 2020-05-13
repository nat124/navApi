using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class TrackOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Status { get; set; }
        public string TrackId { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }

        //public virtual Checkout OrderDetail { get; set; }

    }
}