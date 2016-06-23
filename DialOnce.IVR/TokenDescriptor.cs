using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DialOnce.IVR
{
    public class TokenDescriptor
    {
        public string Token { get; set;}
        public string Scheme { get; set;}
        public DateTime Expires { get; set;}

        public TokenDescriptor(string token, string scheme, DateTime expires) {
            this.Token = token;
            this.Scheme = scheme;
            this.Expires = expires;

        }
    }
}
