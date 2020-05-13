using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCore
{
    public class ResponseModel
    {
        public int ReturnCode { get; set; }
        public string ReturnMessage { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public int RoleId { get; set; }
    }
}
