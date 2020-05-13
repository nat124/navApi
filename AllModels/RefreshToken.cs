using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class RefreshTokens
    {

        public int Id { get; set; }
        public string Email { get; set; }
        public string Refreshtoken { get; set; }
        public bool Revoked { get; set; }
        
    }
}
