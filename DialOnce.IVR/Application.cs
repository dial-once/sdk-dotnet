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

        private HttpClient httpClient = new HttpClient();

        public Application(string clientId, string clientSecret)
        {

            if (this.Token == null || String.IsNullOrEmpty(this.Token.Token)) {
                this.Token = GetTokenDescriptor(clientId, clientSecret);
            }

        }

        private TokenDescriptor GetTokenDescriptor(string clientId, string clientSecret)
        {
            if (String.IsNullOrEmpty(clientId)) throw new Exception("clientId must not be null or empty");
            if (String.IsNullOrEmpty(clientSecret)) throw new Exception("clientSecret must not be null or empty");


            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("client_id", clientId));
            postData.Add(new KeyValuePair<string, string>("client_secret", clientSecret));

            HttpContent content = new FormUrlEncodedContent(postData);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");


            var request = new HttpRequestMessage(HttpMethod.Post, Properties.Resources.TOKEN_URL);
            request.Content = content;


            HttpResponseMessage response = httpClient.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();
            string result = response.Content.ReadAsStringAsync().Result;
            TokenResult tokenResult = JsonConvert.DeserializeObject<TokenResult>(result);
            return new TokenDescriptor(tokenResult.access_token, tokenResult.token_type, tokenResult.expire_at);

        }

        public Application(TokenDescriptor token)
        {
            this.Token = token;

        }
    }
}
