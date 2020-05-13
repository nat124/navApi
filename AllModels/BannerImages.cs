using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
   public class BannerImages
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
     // public string Place { get; set; }
    //  public  int Order { get; set; }
    public string Side { get; set; }
        public string View { get; set; }
        public int Position { get; set; }
        public bool IsActive { get; set; }
    }
}
