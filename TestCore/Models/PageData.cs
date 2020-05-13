using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCore
{
    public class PageData
    {
        public int UserId { get; set; }
        public string IpAddress { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
