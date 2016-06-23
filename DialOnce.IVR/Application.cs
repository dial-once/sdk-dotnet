using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace DialOnce
{
    public class Application
    {
        public TokenDescriptor Token { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }

        private HttpClient httpClient = new HttpClient();

        public Application(string clientId, string clientSecret)
        {
            this.ClientId = clientId;
            this.ClientSecret = clientSecret;
        }

        public Application(TokenDescriptor token)
        {
            this.Token = token;

        }
    }
}
