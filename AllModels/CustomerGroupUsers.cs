using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
   public  class CustomerGroupUsers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CustomerGroupId { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; }
        public virtual CustomerGroup CustomerGroup { get; set; }
        public virtual User User { get; set; }
    }
}
