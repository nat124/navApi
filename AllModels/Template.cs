using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
   public class Template
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Templatetype { get; set; }
        public string TemplateSubject { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string TemplateContent { get; set; }
        public bool IsActive { get; set; }
    }
}
