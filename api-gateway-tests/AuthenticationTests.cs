using api_gateway_tests.setting;
using AutoFixture;
using Microsoft.AspNetCore.Mvc.Testing;
using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;

namespace api_gateway_tests
{
    public class AuthenticationTests : IDisposable
    {
        private HttpClient _httpClient;
        private string _testEnvironmentUrl;
        private CustomWebApplicationFactory<Program> _factory;
        private const string _clientSecret = "0Cq5KAWWtg9e97O5EnQLMEuegBT3OPLj";
        private const string _clientId = "spiegelspel";
        private const string _adminPassword = "jHwS2GNeO1k7OJ4wYx1M";
        private LoginToken _adminToken;

        private readonly KeycloakUser _user = new KeycloakUser()
        {
            Username = "username",
            Firstname = "firstname",
            Lastname = "lastname",
            Email = "newuser@example.com",
        };

        private readonly KeycloakUser _updatedUser = new KeycloakUser()
        {
            Firstname = "updatedFirstname",
            Lastname = "updatedlastname",
            Email = "updatedemail@example.com",
        };

        public AuthenticationTests()
        {
            _factory = new CustomWebApplicationFactory<Program>();
            _testEnvironmentUrl = "http://localhost:80";
            _httpClient = _factory.CreateClient(new WebApplicationFactoryClientOptions());
        }

        public LoginToken GetAdminToken()
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

        [SetUp]
        public  void AdminLogin()
        {
            _adminToken = GetAdminToken();
        }

        private async Task CreateUser()
        {
            string endpoint = "/api/register";

            // User data to be sent in the request body
            string userDataJson = $@"  
            {{
                ""username"": ""{_user.Username}"",
                ""email"": ""{_user.Email}"",
                ""enabled"": true,
                ""firstName"": ""{_user.Firstname}"",
                ""lastName"": ""{_user.Lastname}"",
                ""credentials"": [
                    {{
                        ""type"": ""password"",
                        ""value"": ""1234"",
                        ""temporary"": false
                    }}
                ]
            }}";

            // Set the Authorization header with client credentials
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken.AccessToken);
            var url = _testEnvironmentUrl + endpoint;
            var response = await _httpClient.PostAsync(url, new StringContent(userDataJson, Encoding.UTF8, "application/json"));
            await GetUserId();
        }

        private async Task GetUserId()
        {
            string endpoint = "/api/getUser";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken.AccessToken);

            var response = await _httpClient.GetAsync(_testEnvironmentUrl + endpoint + $"?email={_user.Email}");
            string responseContent = response.Content.ReadAsStringAsync().Result;
           List<KeycloakUser> user = JsonConvert.DeserializeObject<List<KeycloakUser>>(responseContent);
           _user.Id = user[0].Id;
        }



        [Test]
        public async Task LoginOfAUser_ShouldReturnOk()
        {
            //Arrange 
            string endpoint = "/api/login";

            var formData = new Dictionary<string, string>
            {
                { "grant_type", "password" },
                { "client_id", _clientId },
                { "client_secret", _clientSecret },
                { "username","test"},
                { "password","1234" }
            };

            //Act
            var url = _testEnvironmentUrl + endpoint;
            HttpResponseMessage response = await _httpClient.PostAsync(url, new FormUrlEncodedContent(formData));
            string responseContent = response.Content.ReadAsStringAsync().Result;
            LoginToken loginInfo = JsonConvert.DeserializeObject<LoginToken>(responseContent);
            //Assert
            Assert.That(response.IsSuccessStatusCode, Is.True);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Content, Is.Not.Null);
            Assert.That(responseContent, Does.Contain("access_token"));
            Assert.That(responseContent, Does.Contain("refresh_token"));
            Assert.That(loginInfo.AccessToken, Is.Not.Null);
            Assert.That(loginInfo.RefreshToken, Is.Not.Null);

        }

        [Test]
        public async Task RegistrationOfAUser_ShouldReturn_Created()
        {
            //Arrange 

            string endpoint = "/api/register";

            // User data to be sent in the request body
            string userDataJson = $@"  
            {{
                ""username"": ""{_user.Username}"",
                ""email"": ""{_user.Email}"",
                ""enabled"": true,
                ""firstName"": ""{_user.Firstname}"",
                ""lastName"": ""{_user.Lastname}"",
                ""credentials"": [
                    {{
                        ""type"": ""password"",
                        ""value"": ""1234"",
                        ""temporary"": false
                    }}
                ]
            }}";

            // Set the Authorization header with client credentials
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken.AccessToken);

            var url = _testEnvironmentUrl + endpoint;

            //Act
            var response = await _httpClient.PostAsync(url, new StringContent(userDataJson, Encoding.UTF8, "application/json"));

            //Assert
           // Assert.That(response.IsSuccessStatusCode, Is.True);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(response.Headers.Contains("Location"));

            //Teardown
            await End();
        }

        [Test]
        public async Task GetUserByEmail_ShouldReturnUser()
        {
            //Arrange 
            await CreateUser();
            string endpoint = "/api/getUser";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken.AccessToken);

            //Act
            var response = await _httpClient.GetAsync(_testEnvironmentUrl + endpoint + $"?email={_user.Email}");
            string responseContent = response.Content.ReadAsStringAsync().Result;
            List<KeycloakUser> user = JsonConvert.DeserializeObject<List<KeycloakUser>>(responseContent);

            //Assert
            // Assert.That(response.IsSuccessStatusCode, Is.True);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(responseContent, Is.Not.EqualTo(""));
            Assert.That(user[0].Id, Is.Not.EqualTo(""));
            Assert.That(user[0].Firstname, Is.EqualTo(_user.Firstname));
            Assert.That(user[0].Lastname, Is.EqualTo(_user.Lastname));
            Assert.That(user[0].Email, Is.EqualTo(_user.Email));
            Assert.That(user[0].Username, Is.EqualTo(_user.Username));

            //Teardown
            await End();
        }

        [Test]
        public async Task AddingRoleToUser_ShouldReturnSuccess()
            {
            //Arrange 
            await CreateUser();

            string endpoint = "/api/userRole/";

            string jsonData = @"[
            {
                ""id"": ""819933f8-6dd8-4afb-b9ba-177e5d0d6ec3"",
                ""name"": ""user""
            }
         ]";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken.AccessToken);

            //Act
            var url = _testEnvironmentUrl + endpoint + _user.Id;


            var response = await _httpClient.PostAsync(url, new StringContent(jsonData, Encoding.UTF8, "application/json"));

            //Assert
            Assert.That(response.IsSuccessStatusCode, Is.True);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));

            //Teardown
            await End();

        }

        [Test]
        public async Task UpdateOfAUser_ShouldReturnSuccess()
        {
            //Arrange 
            await CreateUser();
            string endpoint = "/api/updateUser/";

            string updatedUserDataJson = $@"  
            {{
                ""email"": ""{_updatedUser.Email}"",
                ""firstName"": ""{_updatedUser.Firstname}"",
                ""lastName"": ""{_updatedUser.Lastname}""
            }}";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken.AccessToken);

            //Act
            var response = await _httpClient.PutAsync(_testEnvironmentUrl + endpoint + _user.Id, new StringContent(updatedUserDataJson, Encoding.UTF8, "application/json"));

            var getResponse = await _httpClient.GetAsync(_testEnvironmentUrl + "/api/getUser/" + _user.Id);
            string responseContent = getResponse.Content.ReadAsStringAsync().Result;


            //Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
            Assert.That(responseContent.Contains(_updatedUser.Firstname));
            Assert.That(responseContent.Contains(_updatedUser.Lastname));
            Assert.That(responseContent.Contains(_user.Username));
            Assert.That(responseContent.Contains(_updatedUser.Email));

            //Teardown
            await End();
        }

        [Test]
        public async Task GetUserById_ShouldReturnUser()
        {
            //Arrange 
            await CreateUser();
            string endpoint = "/api/getUser/";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken.AccessToken);

            //Act
            var response = await _httpClient.GetAsync(_testEnvironmentUrl + endpoint + _user.Id);
            string responseContent = response.Content.ReadAsStringAsync().Result;
            KeycloakUser user = JsonConvert.DeserializeObject<KeycloakUser>(responseContent);

            //Assert
            Assert.That(response.IsSuccessStatusCode, Is.True);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(user.Id, Is.Not.EqualTo(""));
            Assert.That(user.Firstname, Is.EqualTo(_user.Firstname));
            Assert.That(user.Lastname, Is.EqualTo(_user.Lastname));
            Assert.That(user.Email, Is.EqualTo(_user.Email));
            Assert.That(user.Username, Is.EqualTo(_user.Username));

            //Teardown
            await End();
        }

        [Test]
        public async Task DeleteUser_ShouldReturnSuccess()
        {
            //Arrange 
            await CreateUser();
            string endpoint = "/api/deleteUser/";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken.AccessToken);

            //Act
            var response = await _httpClient.DeleteAsync(_testEnvironmentUrl + endpoint + _user.Id);
            var getResponse = await _httpClient.GetAsync(_testEnvironmentUrl + endpoint + _user.Id);

            //Assert
            Assert.That(response.IsSuccessStatusCode, Is.True);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
            Assert.That(getResponse.IsSuccessStatusCode, Is.False);
            Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));

        }

        [Test]
        public async Task LogoutUser_ShouldReturnSuccess()
        {
            //Arrange 
            string endpoint = "/api/logout";
            

            var formData = new Dictionary<string, string>
            {
                { "client_id", _clientId },
                { "client_secret", _clientSecret },
                { "refresh_token",_adminToken.RefreshToken},
            };

            //Act
            var response = await _httpClient.PostAsync(_testEnvironmentUrl + endpoint, new FormUrlEncodedContent(formData));

            //Assert
            Assert.That(response.IsSuccessStatusCode, Is.True);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }

        public async Task End()
        {
            if (_user.Id == null || _user.Id == "")
            {
                //Teardown
                await GetUserId();

            }
                string endpoint = "/api/deleteUser/";
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken.AccessToken);
               var response = await _httpClient.DeleteAsync(_testEnvironmentUrl + endpoint + _user.Id);
        }

        [TearDown]
        public async Task AdminLogout()
        {
            //Arrange 
            string endpoint = "/api/logout";
            var formData = new Dictionary<string, string>
            {
                { "client_id", _clientId },
                { "client_secret", _clientSecret },
                { "refresh_token",_adminToken.RefreshToken},
            };

            //Act
            var response = await _httpClient.PostAsync(_testEnvironmentUrl + endpoint, new FormUrlEncodedContent(formData));
        }

        public void Dispose()
        {
            _httpClient.Dispose();
            _factory.Dispose();
        }
    }
}
