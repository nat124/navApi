using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Testimonial
    {
       
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsActive { get; set; }
        public virtual User User { get; set; }
    }
}
