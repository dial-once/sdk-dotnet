using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DialOnce.IVR
{
   public class TokenResult
    {
        public string access_token { get; set; }
        public string token_type {get; set; }
        public DateTime expire_at { get; set; }

        public string message { get; set; }
        public int status { get; set; }
    }
}
