using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
 public    class SpinnerOptionsPeriod
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
       public int SpinnerPromotionId { get; set; }
        public int Chances { get; set; }
        public string Period { get; set; }

        public bool IsActive { get; set; }
        public virtual SpinnerPromotion SpinnerPromotion { get; set; }

    }
}
