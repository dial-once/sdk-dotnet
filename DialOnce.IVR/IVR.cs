using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DialOnce
{
    public class IVR
    {
        public struct LogType
        {
            public static readonly string CALL_START = "call-start";
            public static readonly string CALL_END = "call-end";
            public static readonly string ANSWER_GET_SMS = "answer-get-sms";
            public static readonly string ANSWER_NO_SMS = "answer-no-sms";

            public string Value { get; set; }

            public LogType(String value)
            {
                this.Value = value;
            }
        };

        private HttpClient httpClient;

        private Application app { get; set; }
        private string caller { get; set; }
        private string called { get; set; }

        public IVR(Application app, string caller, string called)
        {

            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(Properties.Resources.BASE_URL);

            this.app = app;
            this.caller = caller;
            this.called = called;
        }

        public IVR(Application app)
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(Properties.Resources.BASE_URL);
            this.app = app;
        }


        public IVR Init()
        {
            if (this.app.Token == null || String.IsNullOrEmpty(this.app.Token.Token))
            {
                this.app.Token = GetTokenDescriptor(app.ClientId, app.ClientSecret);
                this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(this.app.Token.Scheme, this.app.Token.Token);
            }

            return this;
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


            var request = new HttpRequestMessage(HttpMethod.Post, Properties.Resources.TOKEN_ENDPOINT);
            request.Content = content;


            HttpResponseMessage response = httpClient.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();
            string result = response.Content.ReadAsStringAsync().Result;
            dynamic tokenResult = JsonConvert.DeserializeObject(result);
            DateTime expireAt = Convert.ToDateTime(tokenResult.expire_at);
            return new TokenDescriptor(Convert.ToString(tokenResult.access_token), Convert.ToString(tokenResult.token_type), expireAt);

        }


        public bool Log(LogType logType)
        {
            var postData = new List<KeyValuePair<string, string>>(3);
            postData.Add(new KeyValuePair<string, string>("type", logType.Value));
            postData.Add(new KeyValuePair<string, string>("called", this.called));
            postData.Add(new KeyValuePair<string, string>("caller", this.caller));

            HttpContent content = new FormUrlEncodedContent(postData);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Properties.Resources.LOG_ENDPOINT);
            request.Content = content;
            request.Headers.Authorization = new AuthenticationHeaderValue(this.app.Token.Scheme, this.app.Token.Token);

            HttpResponseMessage response = this.httpClient.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();
            string result = response.Content.ReadAsStringAsync().Result;
            dynamic resultObj = JsonConvert.DeserializeObject(result);
            return resultObj.success;
        }

        public bool IsMobile(string cultureISO = "")
        {
            UriBuilder builder = new UriBuilder(Properties.Resources.BASE_URL);
            builder.Path = Properties.Resources.IS_MOBILE_ENDPOINT;

            string url = builder.Uri.ToString() + @"?number=" + this.caller;

            if (!String.IsNullOrEmpty(cultureISO))
            {
                url += @"&cultureISO=" + cultureISO;
            }

            HttpResponseMessage response = this.httpClient.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            string result = response.Content.ReadAsStringAsync().Result;
            dynamic resultObj = JsonConvert.DeserializeObject(result);
            return resultObj.mobile;
        }

        public bool IsEligible()
        {
            UriBuilder builder = new UriBuilder(Properties.Resources.BASE_URL);
            builder.Path = Properties.Resources.IS_ELEGIBLE_ENDPOINT;

            string url = builder.Uri.ToString() + @"?caller=" + this.caller.Trim() + @"&called=" + this.caller.Trim();

            HttpResponseMessage response = this.httpClient.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            string result = response.Content.ReadAsStringAsync().Result;
            dynamic resultObj = JsonConvert.DeserializeObject(result);
            return resultObj.eligible;
        }


        public bool ServiceRequest()
        {
            var postData = new List<KeyValuePair<string, string>>(3);
            postData.Add(new KeyValuePair<string, string>("called", this.called));
            postData.Add(new KeyValuePair<string, string>("caller", this.caller));

            HttpContent content = new FormUrlEncodedContent(postData);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");


            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Properties.Resources.SERVICE_REQUEST_ENDPOINT);
            request.Content = content;
            request.Headers.Authorization = new AuthenticationHeaderValue(this.app.Token.Scheme, this.app.Token.Token);

            HttpResponseMessage response = this.httpClient.SendAsync(request).Result;
            response.EnsureSuccessStatusCode();
            string result = response.Content.ReadAsStringAsync().Result;
            dynamic resultObj = JsonConvert.DeserializeObject(result);
            return resultObj.success;
        }
    }

}
