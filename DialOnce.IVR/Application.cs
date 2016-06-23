using System.Net.Http;

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
