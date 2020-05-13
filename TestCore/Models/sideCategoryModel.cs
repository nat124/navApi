using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCore
{
    public class SideCategoryModel
    {
        public SideCategoryModel()
        {
            Children = new List<ProductCategory>();
            OtherIdlevel2 = new List<ProductCategory>();
        }
        public int Idlevel1 { get; set; }
        public int ChildId { get; set; }
        public string Namelevel1 { get; set; }
        public string SpanishNamelevel1 { get; set; }
        public string ChildName { get; set; }
        public string SpanishChildName { get; set; }
        public int Idlevel2 { get; set; }
        public string Namelevel2 { get; set; }
        public string SpanishNamelevel2 { get; set; }
        public List<ProductCategory>  Children { get; set; }
        public List<ProductCategory> OtherIdlevel2 { get; set; }
    }
}
