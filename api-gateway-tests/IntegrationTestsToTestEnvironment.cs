using api_gateway_tests.setting;
using Keycloak.Net.Models.Clients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Models;
using Newtonsoft.Json;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace api_gateway_tests
{
    public class IntegrationTestsToTestEnvironment : IDisposable
    {
        private HttpClient _httpClient;
        private string _testEnvironmentUrl;
        private CustomWebApplicationFactory<Program> _factory;
        private const string _clientSecret = "9DEnqYDcg75BHfTXAmPYW09orkDy0Rcg";
        private const string _clientId = "spiegelspel";

        public IntegrationTestsToTestEnvironment()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            _testEnvironmentUrl = "http://localhost:80";
            _httpClient = _factory.CreateClient(new WebApplicationFactoryClientOptions());
        }

        private LoginToken GetAdminToken()
        {
            string endpoint = "/api/login";
            var formData = new Dictionary<string, string>
        {
            { "grant_type", "password" },
            { "client_id", "spiegelspel" },
            { "client_secret", "9DEnqYDcg75BHfTXAmPYW09orkDy0Rcg" },
            { "username","admin"},
             { "password","jHwS2GNeO1k7OJ4wYx1M" }
            };

            var url = _testEnvironmentUrl + endpoint;
            HttpResponseMessage response =  _httpClient.PostAsync(url, new FormUrlEncodedContent(formData)).Result;
            string responseContent = response.Content.ReadAsStringAsync().Result;
            LoginToken myArray = JsonConvert.DeserializeObject<LoginToken>(responseContent);
            return myArray;
        }

        [SetUp]
        public void StartServer()
        {


            //Arrange 

            //Act
            //Assert

        }

        [Test]

        public async Task RegistrationOfAUser_ShouldReturn_Created()
        {
            //Arrange 

            //log in admin user 
            string endpoint = "/api/register";

            // User data to be sent in the request body
            string userDataJson = @"
            {
                ""username"": ""newuser2"",
                ""email"": ""newuser2@example.com"",
                ""enabled"": true,
                ""firstName"": ""New2"",
                ""lastName"": ""User2"",
                ""credentials"": [
                    {
                        ""type"": ""password2"",
                        ""value"": ""userpassword2"",
                        ""temporary"": false
                    }
                ]
            }";

            var token =  GetAdminToken().AccessToken;

            // Set the Authorization header with client credentials
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var url = _testEnvironmentUrl + endpoint;

            //Act
            var response = await _httpClient.PostAsync(url, new StringContent(userDataJson, Encoding.UTF8, "application/json"));



            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

        [Test]
        [HttpPost()]
        public async Task LoginOfAUser_ShouldReturnSuccess()
        {
            //Arrange 

            string endpoint = "/api/login";

            var formData = new Dictionary<string, string>
        {
            { "grant_type", "password" },
            { "client_id", "spiegelspel" },
            { "client_secret", "9DEnqYDcg75BHfTXAmPYW09orkDy0Rcg" },
            { "username","fkwapong"},
             { "password","1234" }
            };

            //Act
            var url = _testEnvironmentUrl + endpoint;
            HttpResponseMessage response = await _httpClient.PostAsync(url, new FormUrlEncodedContent(formData));

            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));


        }


        public void Dispose()
        {
            _httpClient.Dispose();
            _factory.Dispose();
        }
    }
}
