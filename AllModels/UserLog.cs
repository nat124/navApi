using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
   public  class UserLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? ActionId { get; set; }
        public int? ProductId { get; set; }
        public int? PageId { get; set; }
        public string IPAddress { get; set; }
        public string Url { get; set; }
        public DateTime? LogInDate { get; set; }
        public DateTime? LogOutDate { get; set; }
        public bool IsActive { get; set; }
    public virtual Action Action { get; set; }
        public virtual Page Page { get; set; }
        public virtual User User { get; set; }
        public virtual Product Product { get; set; }
    }
}
