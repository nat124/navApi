using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
  public  class SpinnerPromotion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Image { get; set; }
        public string DisplayMessage { get; set; }
        public int? ProductCategoryId { get; set; }
        public int? CategoryId { get; set; }
        public int? MoodId { get; set; }
        [NotMapped]
        public string ActiveTos { get; set; }
        [NotMapped]
        public string ActiveFroms { get; set; }
        public DateTime? ActiveTo { get; set; }
        public DateTime? ActiveFrom { get; set; }
        public int? DiscountPrice { get; set; }
        public int? DiscountPercentage { get; set; }
        public bool? IsActive { get; set; }
        public string Description { get; set; }
        public string ActiveFromTime { get; set; }
        public string ActiveToTime { get; set; }
        public int? ProductId { get; set; }
        public string Filterurl { get; set; }
        public int? MaxQty { get; set; }

        public virtual Mood Mood { get; set; }
        public virtual SpinnerOptionsPeriod SpinnerOptionsPeriod { get; set; }

    }
}
