//using api_gateway_tests.setting;
//using Microsoft.AspNetCore.Mvc.Testing;
//using Models;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace api_gateway_tests
//{
//    public class BaseTestClass : IDisposable
//    {
//        protected HttpClient _httpClient;
//        protected string _testEnvironmentUrl;
//        protected CustomWebApplicationFactory<Program> _factory;
//        protected const string _clientSecret = "Sqtplt6kSwTqtXubulAQv7dEuj7Byoyi";
//        protected const string _clientId = "spiegelspel";
//        protected const string _adminPassword = "passworddev";
//        protected Random _random;
//        protected LoginToken _adminToken;

//        public BaseTestClass()
//        {
//            _factory = new CustomWebApplicationFactory<Program>();
//            _testEnvironmentUrl = "http://localhost:80";
//            _httpClient = _factory.CreateClient(new WebApplicationFactoryClientOptions());
//            _random = new Random();
//        }

//        protected LoginToken GetAdminToken()
//        {
//            string endpoint = "/api/login";
//            var formData = new Dictionary<string, string>
//            {
//                { "grant_type", "password" },
//                { "client_id", _clientId },
//                { "client_secret", _clientSecret },
//                { "username","admin"},
//                { "password", _adminPassword }
//            };

//            var url = _testEnvironmentUrl + endpoint;
//            HttpResponseMessage response = _httpClient.PostAsync(url, new FormUrlEncodedContent(formData)).Result;
//            string responseContent = response.Content.ReadAsStringAsync().Result;
//            LoginToken loginInfo = JsonConvert.DeserializeObject<LoginToken>(responseContent);
//            return loginInfo;
//        }


//        public void Dispose()
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
