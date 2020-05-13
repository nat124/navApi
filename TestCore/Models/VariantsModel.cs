using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCore
{
    public class VariantsModel
    {
        public VariantsModel()
        {
            List<VariantOption> VariantOptions = new List<VariantOption>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<VariantOption> VariantOptions { get; set; }
        public bool IsMain { get; set; }
        public int SelectedValue { get; set; }
        public bool IsActive { get; set; }
    }
}
