using Models;
using Newtonsoft.Json;
using System.Net;

namespace api_gateway_tests
{
    public class BaseTestClass : IDisposable
    {
        protected HttpClient _httpClient;
        protected const string _testEnvironmentUrl = "https://api-gateway-route-mg-development.apps.ocp5-inholland.joran-bergfeld.com";
        protected const string _clientSecret = "JTr6gXberP0ovGgvV2KXRHihGNr8cgRq";
        protected const string _clientId = "spiegelspel";
        protected const string _adminPassword = "passworddev";
        protected Random _random;
        protected LoginToken _adminToken;

        public BaseTestClass()
        {
            _random = new Random();
            ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;
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
