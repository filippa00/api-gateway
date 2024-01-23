using Models;
using Newtonsoft.Json;

namespace api_gateway_tests
{
    public class BaseTestClass : IDisposable
    {
        protected HttpClient _httpClient;
        protected string _testEnvironmentUrl;
        protected const string _clientSecret = "Sqtplt6kSwTqtXubulAQv7dEuj7Byoyi";
        protected const string _clientId = "spiegelspel";
        protected const string _adminPassword = "passworddev";
        protected Random _random;
        protected LoginToken _adminToken;

        public BaseTestClass()
        {
            _testEnvironmentUrl = "https://api-gateway-route-mg-development.apps.ocp5-inholland.joran-bergfeld.com";
            _random = new Random();
            _httpClient = new HttpClient(new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            });
        }

        protected LoginToken GetAdminToken()
        {
            string endpoint = "/api/login";
            var formData = new Dictionary<string, string>
            {
                { "grant_type", "password" },
                { "client_id", _clientId },
                { "client_secret", _clientSecret },
                { "username","admin"},
                { "password", _adminPassword }
            };

            var url = _testEnvironmentUrl + endpoint;
            HttpResponseMessage response = _httpClient.PostAsync(url, new FormUrlEncodedContent(formData)).Result;
            string responseContent = response.Content.ReadAsStringAsync().Result;
            LoginToken loginInfo = JsonConvert.DeserializeObject<LoginToken>(responseContent);
            return loginInfo;
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
