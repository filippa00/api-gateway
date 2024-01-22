//using api_gateway_tests.setting;
//using Microsoft.AspNetCore.Mvc.Testing;
//using Models;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http.Headers;
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;
//using Wmhelp.XPath2.MS;

//namespace api_gateway_tests
//{
//    public class CardSetsTests : BaseTestClass
//    {
//        [SetUp]
//        public void AdminLogin()
//        {
//            _adminToken = GetAdminToken();
//        }

//        [Test]
//        public async Task GetCardSets_ShouldReturnCardSets_WithStatusOk()
//        {
//            //Arrange 
//            string endpoint = "/api/CardSets";
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
//            Assert.That(responseContent, Does.Contain("description"));
//            Assert.That(responseContent, Does.Contain("cards"));
//        }

//        [Test]
//        public async Task PostToCardsSets_ShouldReturnNewCardSet_WithStatusCreated()
//        {
//            //Arrange 
//            string endpoint = "/api/CardSets";



//            int nameNr = _random.Next(0, 1000);
//            int descriptionNr = _random.Next(0, 1000);

//            // Cards data to be sent in the request body
//            string cardsDataJson = $@"  
//            {{
//                ""name"": ""New Cardset from api gateway integration test {nameNr}"",
//                ""description"": ""Description for the cardsetfrom api gateway integration test {descriptionNr}"",
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
//            Assert.That(responseContent, Does.Contain($"New Cardset from api gateway integration test {nameNr}"));
//            Assert.That(responseContent, Does.Contain($"Description for the cardsetfrom api gateway integration test {descriptionNr}"));
//        }

//        [TestCase(1)]
//        [TestCase(2)]
//        public async Task GetCardSetsById_ShouldReturnACardSet_WithStatusOk(int id)
//        {
//            //Arrange 
//            string endpoint = "/api/CardSets/";
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
//            Assert.That(responseContent, Does.Contain("description"));
//            Assert.That(responseContent, Does.Contain("cards"));
//        }

//        [TestCase(2)]
//        public async Task PutCardsSetsById_ShouldReturnOk(int id)
//        {
//            //Arrange 
//            string endpoint = "/api/CardSets/";

//            var nameTest = _random.Next(0, 1000);
//            var colorTest = _random.Next(0, 1000);
//            var descriptionTest = _random.Next(0, 1000);
//            var fontTypeTest = _random.Next(0, 1000);

//            string cardsDataJson = $@"  
//            {{
//                ""name"": ""renamecardsSet{nameTest}"",
//                ""color"": ""#121212{colorTest}"",
//                ""description"": ""Description fo the cardset{descriptionTest}"",
//                ""fontType"": ""Arial{fontTypeTest}"",
//            }}";

//            // Set the Authorization header with client credentials
//            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken.AccessToken);

//            //Act
//            var url = _testEnvironmentUrl + endpoint + id;
//            var response = await _httpClient.PutAsync(url, new StringContent(cardsDataJson, Encoding.UTF8, "application/json"));
//            HttpResponseMessage getUpdatedCard = await _httpClient.GetAsync(url);
//            string getUpdatedCardResponse = getUpdatedCard.Content.ReadAsStringAsync().Result;


//            //Assert
//            Assert.That(response.IsSuccessStatusCode, Is.True);
//            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
//            Assert.That(response.Content, Is.Not.Null);
//            Assert.That(getUpdatedCardResponse, Does.Contain($"renamecardsSet{nameTest}"));
//            Assert.That(getUpdatedCardResponse, Does.Contain($"#121212{colorTest}"));
//            Assert.That(getUpdatedCardResponse, Does.Contain($"Description fo the cardset{descriptionTest}"));
//            Assert.That(getUpdatedCardResponse, Does.Contain($"Arial{fontTypeTest}"));

//        }

//        [Test]
//        public async Task DeleteCardSet_ShouldReturnOk()
//        {
//            //Arrange 
//            string endpoint = "/api/CardSets/2";

//            // Set the Authorization header with client credentials
//            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken.AccessToken);

//            //Act
//            var url = _testEnvironmentUrl + endpoint;
//            var response = await _httpClient.DeleteAsync(url);
//            HttpResponseMessage getUpdatedCard = await _httpClient.GetAsync(url);

//            //HttpResponseMessage getAllCards = await _httpClient.GetAsync(url);
//            //string allCardsContent = getAllCards.Content.ReadAsStringAsync().Result;

//            //Assert
//            Assert.That(response.IsSuccessStatusCode, Is.True);
//            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
//            Assert.That(getUpdatedCard.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));

//        }

//        [TestCase(2)]
//        public async Task PutCardsSetsLogoById_ShouldReturnOk_WithTheLogo(int id)
//        {
//            //Arrange 
//            string endpoint = "/api/CardSets/";

//            var logoTest = _random.Next(0, 1000);

//            string logoDataJson = $@"  
//            {{
//                ""logo"": ""cardsSetLogo{logoTest}"",
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
//            Assert.That(getUpdatedLogoResponse, Does.Contain($"cardsSetLogo{logoTest}"));

//        }
//    }
//}

