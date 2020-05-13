using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
  public  class SpinUserData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }//hai
        public int SpinnerPromotionId { get; set; }//hai
        public int? SpinCount { get; set; }//hai
        public bool IsActive { get; set; }
        public virtual User User { get; set; }
        public bool? IsUsed { get; set; }
        public int? MoodId { get; set; }//hai
        public DateTime? SpinDate { get; set; }

        public int? CancelCounter { get; set; }
        public DateTime? CancelDate { get; set; }
        public virtual Mood Mood { get; set; }
        public virtual SpinnerPromotion SpinnerPromotion { get; set; }
    }
}
