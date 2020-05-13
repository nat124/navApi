using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Log
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Guid { get; set; }
        public int? LogtypeId { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
        public string Result { get; set; }
        public DateTime? ActionDateTime { get; set; }
        public bool IsActive { get; set; }
        public string  IpAddress { get; set; }
        public string RequestUrl { get; set; }
        public string PageUrl { get; set; }
        public bool? IsDesktop { get; set; }
        public string AppVersion { get; set; }
        public string Country { get; set; }
        public virtual Logtype Logtype { get; set; }
        public virtual User User { get; set; }



    }
}
