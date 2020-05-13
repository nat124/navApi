using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCore
{
    public class Filter
    {
        public Filter()
        {
            SelectedVariants = new List<SelectedVariant>();
        }
        public string MinPrice { get; set; }
        public string MaxPrice { get; set; }
        public List<SelectedVariant> SelectedVariants { get; set; }
        public int CategoryId { get; set; }
        public string SearchData { get; set; }
        public int AvgRate { get; set; }
        public string SortBy { get; set; }
    }
    public class SelectedVariant
    {
        public int CategoryId { get; set; }
        public int VariantId { get; set; }
        public List<int> VariantOptionId { get; set; }
    }

}
