using Microsoft.AspNetCore.Mvc.Testing;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace api_gateway_tests
{
    public class CardsTests : BaseTestClass
    {
        [SetUp]
        public void AdminLogin()
        {
            _adminToken = GetAdminToken();
        }

//        [Test]
//        public async Task GetCards_ShouldReturnCards_WithStatusOk()
//        {
//            //Arrange
//            string endpoint = "/api/cards";
//            //Set the Authorization header with client credentials
//            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken.AccessToken);

//            Act
//            var url = _testEnvironmentUrl + endpoint;
//            HttpResponseMessage response = await _httpClient.GetAsync(url);
//            string responseContent = response.Content.ReadAsStringAsync().Result;

//           // Assert
//            Assert.That(response.IsSuccessStatusCode, Is.True);
//            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
//            Assert.That(response.Content, Is.Not.Null);
//            Assert.That(responseContent, Does.Contain("id"));
//            Assert.That(responseContent, Does.Contain("textQuestion"));
//            Assert.That(responseContent, Does.Contain("logoUrl"));
//            Assert.That(responseContent, Does.Contain("imageUrl"));
//            Assert.That(responseContent, Does.Contain("theme"));

        }

//        [Test]
//        public async Task PostToCards_ShouldReturnANewCard_WithStatusCreated()
//        {
//            //Arrange
//            string endpoint = "/api/cards";


            //int TextQuestionNr = _random.Next(0, 1000);
            //int LogoUrlNr = _random.Next(0, 1000);
            //int ImageUrllNr = _random.Next(0, 1000);

//            //Cards data to be sent in the request body
//            string cardsDataJson = $@"  
//            {{
//                ""ThemeId"": ""1"",
//                ""CardSetId"": ""1"",
//                ""TextQuestion"": ""What is the capital of France{TextQuestionNr}?"",
//                ""LogoUrl"": ""http://example.com/{LogoUrlNr}/logo.jpg"",
//                ""ImageUrl"": ""http://example.com/{ImageUrllNr}/image.jpg"",
//                ""IsActive"": ""true"",
//                ""IsPrivate"": ""false"",
               
   //         }}";


//            //Set the Authorization header with client credentials
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
//            Assert.That(responseContent, Does.Contain($"What is the capital of France{TextQuestionNr}?"));
//            Assert.That(responseContent, Does.Contain($"http://example.com/{LogoUrlNr}/logo.jpg"));
//            Assert.That(responseContent, Does.Contain($"http://example.com/{ImageUrllNr}/image.jpg"));

        }

//        [Test]
//        [TestCase(1)]
//        [TestCase(2)]
//        public async Task GetCardsById_ShouldReturnCard_WithStatusOk(int id)
//        {
//            //Arrange
//            string endpoint = "/api/cards/";

//            //Set the Authorization header with client credentials
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
//            Assert.That(responseContent, Does.Contain("textQuestion"));
//            Assert.That(responseContent, Does.Contain("logoUrl"));
//            Assert.That(responseContent, Does.Contain("imageUrl"));
//            Assert.That(responseContent, Does.Contain("theme"));

 //       }

//        [Test]
//        [TestCase(1)]
//        public async Task PutCardsById_ShouldReturnOk(int id)
//        {
//           // Arrange
//            string endpoint = "/api/cards/";

            //var nameTest = _random.Next(0, 1000);

            //string cardsDataJson = $@"  
            //{{
            //    ""name"": ""renamecards{nameTest}"",
            //}}";

//            //Set the Authorization header with client credentials
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
//            Assert.That(getUpdatedCardResponse, Does.Contain($"renamecards{nameTest}"));

        //}

//        [Test]
//        public async Task DeleteCards_ShouldReturnOk()
//        {
//            //Arrange
//            string endpoint = "/api/cards/1";

//            //Set the Authorization header with client credentials
//            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken.AccessToken);

//            //Act
//            var url = _testEnvironmentUrl + endpoint;
//            var response = await _httpClient.DeleteAsync(url);
//            HttpResponseMessage getUpdatedCard = await _httpClient.GetAsync(url);

            //HttpResponseMessage getAllCards = await _httpClient.GetAsync(url);
            //string allCardsContent = getAllCards.Content.ReadAsStringAsync().Result;


//            //Assert
//            Assert.That(response.IsSuccessStatusCode, Is.True);
//            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
//            Assert.That(getUpdatedCard.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));


        //}

//        [Test]
//        [TestCase(2)]
//        public async Task GetCardBySetId_ShouldReturnACardset_WithStatusOk(int id)
//        {
//            //Arrange
//            string endpoint = "/api/Cards/cardsets/";
//            //Set the Authorization header with client credentials
//            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken.AccessToken);

//           // Act
//            var url = _testEnvironmentUrl + endpoint + id;
//            HttpResponseMessage response = await _httpClient.GetAsync(url);
//            string responseContent = response.Content.ReadAsStringAsync().Result;

//            //Assert
//            Assert.That(response.IsSuccessStatusCode, Is.True);
//            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
//            Assert.That(response.Content, Is.Not.Null);
//            Assert.That(responseContent, Does.Contain("id"));
//            Assert.That(responseContent, Does.Contain("textQuestion"));
//            Assert.That(responseContent, Does.Contain("logoUrl"));
//            Assert.That(responseContent, Does.Contain("imageUrl"));
//            Assert.That(responseContent, Does.Contain("theme"));

        //}

//        [Test]
//        [TestCase(2)]
//        public async Task GetCardByThemeId(int id)
//        {
//           //Arrange
//            string endpoint = "/api/Cards/themes/";
//            //Set the Authorization header with client credentials
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
//            Assert.That(responseContent, Does.Contain("textQuestion"));
//            Assert.That(responseContent, Does.Contain("logoUrl"));
//            Assert.That(responseContent, Does.Contain("imageUrl"));
//            Assert.That(responseContent, Does.Contain("theme"));

//        }
//    }
//}
