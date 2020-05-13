using Localdb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCore
{
    public class RatingReviewfunctions
    {

        private readonly PistisContext db;
        public RatingReviewfunctions(PistisContext pistis)
        {
            db = pistis;
        }
        public  int AvgRating(int ProductId)
        {
            var data = db.RatingReviews.Where(x => x.IsActive == true && x.ProductId == ProductId).ToList().Select(x => x.Rating);
            var count = data.Count();
            Int32 avg=0;
            if(count>0)
             avg = Convert.ToInt32(data.Sum() / count);
            return avg;
        }
    }
}
