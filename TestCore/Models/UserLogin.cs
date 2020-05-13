using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestCore
{
        public class UserLogin
        {
            public string username { get; set; }
            public string password { get; set; }

        public string  Token { get; set; }
        public string  refreshToken { get; set; }
        public int RoleId { get; set; }
    }

}