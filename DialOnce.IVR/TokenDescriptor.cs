using System;

namespace DialOnce
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
