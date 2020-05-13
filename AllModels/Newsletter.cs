using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Newsletter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string IpAddress { get; set; }
        public string Email { get; set; }
        public bool IsSubscribed { get; set; }
        public bool IsActive { get; set; }

        public int CancelCounter { get; set; }
        public DateTime? Date { get; set; }

        public virtual User User { get; set; }
        [NotMapped]
        public string Content { get; set; }
        [NotMapped]
        public string TemplateName { get; set; }
        [NotMapped]
        public string SenderName { get; set; }
        [NotMapped]
        public string SenderEmail { get; set; }
        [NotMapped]
        public string TemplateSubject { get; set; }
    }
}
