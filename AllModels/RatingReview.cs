using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Models
{
    public class RatingReview
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Rating { get; set; }
        public string Review { get; set; }
        public int UserId { get; set; }
        public int? ReviewStatusId { get; set; }
        public DateTime ReviewDate { get; set; }
        public bool IsActive { get; set; }
        public bool? IsDefault { get; set; }
     //   public bool? IsApproved { get; set; }

        public virtual Product Product { get;set;}
        public virtual User User { get; set; }
        public virtual ReviewStatus ReviewStatus { get; set; }

    }
}
