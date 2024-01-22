//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http.Headers;
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;

//namespace api_gateway_tests
//{
//    public class ThemesTests : BaseTestClass
//    {
//        [SetUp]
//        public void AdminLogin()
//        {
//            _adminToken = GetAdminToken();
//        }

//        [Test]
//        public async Task GetThemes_ShouldReturnThemes_WithStatusReturnOk()
//        {
//            //Arrange 
//            string endpoint = "/api/Themes";
//            // Set the Authorization header with client credentials
//            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken.AccessToken);

//            //Act
//            var url = _testEnvironmentUrl + endpoint;
//            HttpResponseMessage response = await _httpClient.GetAsync(url);
//            string responseContent = response.Content.ReadAsStringAsync().Result;

//            //Assert
//            Assert.That(response.IsSuccessStatusCode, Is.True);
//            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
//            Assert.That(response.Content, Is.Not.Null);
//            Assert.That(responseContent, Does.Contain("id"));
//            Assert.That(responseContent, Does.Contain("name"));
//            Assert.That(responseContent, Does.Contain("color"));
//            Assert.That(responseContent, Does.Contain("description"));
//        }

//        [Test]
//        public async Task PostToThemes_ShouldReturnNewTheme_WithStatusCreated()
//        {
//            //Arrange 
//            string endpoint = "/api/Themes";



//            int idNr = _random.Next(0, 1000);
//            int nameNr = _random.Next(0, 1000);
//            int colorNr = _random.Next(0, 1000);
//            int descriptionNr = _random.Next(0, 1000);

//            // Cards data to be sent in the request body
//            string cardsDataJson = $@"  
//            {{
//                ""id"": ""{idNr}"",
//                ""name"": ""New name for the theme from api gateway integration test {nameNr}"",
//                ""color"": ""#14567 {colorNr}"",
//                ""description"": ""Description for the new theme from api gateway integration test {descriptionNr}"",
//            }}";


//            // Set the Authorization header with client credentials
//            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken.AccessToken);

//            //Act
//            var url = _testEnvironmentUrl + endpoint;
//            var response = await _httpClient.PostAsync(url, new StringContent(cardsDataJson, Encoding.UTF8, "application/json"));
//            HttpResponseMessage getCreatedCard = await _httpClient.GetAsync(url);
//            string responseContent = getCreatedCard.Content.ReadAsStringAsync().Result;

//            //Assert
//            Assert.That(response.IsSuccessStatusCode, Is.True);
//            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
//            Assert.That(response.Content, Is.Not.Null);
//            Assert.That(responseContent, Does.Contain($"{idNr}"));
//            Assert.That(responseContent, Does.Contain($"New name for the theme from api gateway integration test {nameNr}"));
//            Assert.That(responseContent, Does.Contain($"#14567 {colorNr}"));
//            Assert.That(responseContent, Does.Contain($"Description for the new theme from api gateway integration test {descriptionNr}"));
//        }

//        [TestCase(1)]
//        [TestCase(2)]
//        public async Task GetThemesById_ShouldReturnATheme_WithStatusOk(int id)
//        {
//            //Arrange 
//            string endpoint = "/api/Themes/";
//            // Set the Authorization header with client credentials
//            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken.AccessToken);

//            //Act
//            var url = _testEnvironmentUrl + endpoint + id;
//            HttpResponseMessage response = await _httpClient.GetAsync(url);
//            string responseContent = response.Content.ReadAsStringAsync().Result;

//            //Assert
//            Assert.That(response.IsSuccessStatusCode, Is.True);
//            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
//            Assert.That(response.Content, Is.Not.Null);
//            Assert.That(responseContent, Does.Contain("id"));
//            Assert.That(responseContent, Does.Contain("name"));
//            Assert.That(responseContent, Does.Contain("colour"));
//            Assert.That(responseContent, Does.Contain("description"));
//        }

//        [TestCase(2)]
//        public async Task PutThemesById_ShouldReturnOk(int id)
//        {
//            //Arrange 
//            string endpoint = "/api/Themes/";

//            var idTest = _random.Next(0, 1000);
//            var nameTest = _random.Next(0, 1000);
//            var colorTest = _random.Next(0, 1000);
//            var descriptionTest = _random.Next(0, 1000);

//            string cardsDataJson = $@"  
//            {{
//                ""id"": ""{idTest}"",
//                ""name"": ""renametheme{nameTest}"",
//                ""color"": ""#121212{colorTest}"",
//                ""description"": ""Description fo the theme{descriptionTest}"",
//            }}";

//            // Set the Authorization header with client credentials
//            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken.AccessToken);

//            //Act
//            var url = _testEnvironmentUrl + endpoint + id;
//            var response = await _httpClient.PutAsync(url, new StringContent(cardsDataJson, Encoding.UTF8, "application/json"));
//            HttpResponseMessage getUpdatedCard = await _httpClient.GetAsync(url);
//            string getUpdatedThemeResponse = getUpdatedCard.Content.ReadAsStringAsync().Result;


//            //Assert
//            Assert.That(response.IsSuccessStatusCode, Is.True);
//            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
//            Assert.That(response.Content, Is.Not.Null);
//            Assert.That(getUpdatedThemeResponse, Does.Contain($"{idTest}"));
//            Assert.That(getUpdatedThemeResponse, Does.Contain($"renametheme{nameTest}"));
//            Assert.That(getUpdatedThemeResponse, Does.Contain($"#121212{colorTest}"));
//            Assert.That(getUpdatedThemeResponse, Does.Contain($"Description fo the theme{descriptionTest}"));
//        }

//        [Test]
//        public async Task DeleteTheme_ShouldReturnOk()
//        {
//            //Arrange 
//            string endpoint = "/api/Themes/2";

//            // Set the Authorization header with client credentials
//            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken.AccessToken);

//            //Act
//            var url = _testEnvironmentUrl + endpoint;
//            var response = await _httpClient.DeleteAsync(url);
//            HttpResponseMessage geTheme = await _httpClient.GetAsync(url);

//            //HttpResponseMessage getAllCards = await _httpClient.GetAsync(url);
//            //string allCardsContent = getAllCards.Content.ReadAsStringAsync().Result;

//            //Assert
//            Assert.That(response.IsSuccessStatusCode, Is.True);
//            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
//            Assert.That(geTheme.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));

//        }

//        [TestCase(2)]
//        public async Task PutThemesLogoById_ShouldReturnOk(int id)
//        {
//            //Arrange 
//            string endpoint = "/api/Themes/";

//            var logoTest = _random.Next(0, 1000);

//            string logoDataJson = $@"  
//            {{
//                ""logo"": ""ThemesLogo{logoTest}"",
//            }}";

//            // Set the Authorization header with client credentials
//            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken.AccessToken);

//            //Act
//            var url = _testEnvironmentUrl + endpoint + id + "/logo";
//            var response = await _httpClient.PutAsync(url, new StringContent(logoDataJson, Encoding.UTF8, "application/json"));
//            HttpResponseMessage getUpdatedLogo = await _httpClient.GetAsync(url);
//            string getUpdatedLogoResponse = getUpdatedLogo.Content.ReadAsStringAsync().Result;


//            //Assert
//            Assert.That(response.IsSuccessStatusCode, Is.True);
//            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
//            Assert.That(response.Content, Is.Not.Null);
//            Assert.That(getUpdatedLogoResponse, Does.Contain($"ThemesLogo{logoTest}"));

//        }
//    }
//}
