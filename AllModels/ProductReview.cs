using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
  public  class ProductReview
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string NumberofReviews { get; set; }
        public string AvgApprovedRating { get; set; }
    }
}
