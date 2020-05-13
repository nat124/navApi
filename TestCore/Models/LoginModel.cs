using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestCore
{
    public class LoginModel
    {
        public int roleId { get; set; }
        public int id { get; set; }
        public string message { get; set; }
        public string username { get; set; }
        public int IsSubscribed { get; set; }
        public string password { get; set; }
        public bool success { get; set; }
        public string Token { get; set; }
        public string refreshToken { get; set; }
    }
}