using api_gateway_tests.setting;
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

        [Test]
        public async Task GetCards()
        {
            //Arrange 
            string endpoint = "/api/cards";
            // Set the Authorization header with client credentials
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken.AccessToken);

            //Act
            var url = _testEnvironmentUrl + endpoint;
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            string responseContent = response.Content.ReadAsStringAsync().Result;
           
            //Assert
            Assert.That(response.IsSuccessStatusCode, Is.True);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Content, Is.Not.Null);
            Assert.That(responseContent, Does.Contain("id"));
            Assert.That(responseContent, Does.Contain("textQuestion"));
            Assert.That(responseContent, Does.Contain("logoUrl"));
            Assert.That(responseContent, Does.Contain("imageUrl"));
            Assert.That(responseContent, Does.Contain("theme"));
     
        }

        [Test]
        public async Task PostToCards()
        {
            //Arrange 
            string endpoint = "/api/cards";



            // Cards data to be sent in the request body
            string cardsDataJson = $@"  
            {{
                ""ThemeId"": ""1"",
                ""CardSetId"": ""1"",
                ""TextQuestion"": ""What is the capital of France?"",
                ""LogoUrl"": ""http://example.com/logo.jpg"",
                ""ImageUrl"": ""http://example.com/image.jpg"",
                ""IsActive"": ""true"",
                ""IsPrivate"": ""false"",
               
            }}";


            // Set the Authorization header with client credentials
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken.AccessToken);

            //Act
            var url = _testEnvironmentUrl + endpoint;
            var response = await _httpClient.PostAsync(url, new StringContent(cardsDataJson, Encoding.UTF8, "application/json"));
            HttpResponseMessage getCreatedCard = await _httpClient.GetAsync(url);
            string responseContent = getCreatedCard.Content.ReadAsStringAsync().Result;

            //Assert
            Assert.That(response.IsSuccessStatusCode, Is.True);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(response.Content, Is.Not.Null);
            Assert.That(responseContent, Does.Contain("1"));
            Assert.That(responseContent, Does.Contain("What is the capital of France?"));
            Assert.That(responseContent, Does.Contain("http://example.com/logo.jpg"));
            Assert.That(responseContent, Does.Contain("http://example.com/image.jpg"));

        }

        [Test]
        [TestCase()]
        public async Task GetCardsById()
        {
            //Arrange 
            string endpoint = "/api/cards/1";
            // Set the Authorization header with client credentials
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken.AccessToken);

            //Act
            var url = _testEnvironmentUrl + endpoint;
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            string responseContent = response.Content.ReadAsStringAsync().Result;

            //Assert
            Assert.That(response.IsSuccessStatusCode, Is.True);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Content, Is.Not.Null);
            Assert.That(responseContent, Does.Contain("id"));
            Assert.That(responseContent, Does.Contain("textQuestion"));
            Assert.That(responseContent, Does.Contain("logoUrl"));
            Assert.That(responseContent, Does.Contain("imageUrl"));
            Assert.That(responseContent, Does.Contain("theme"));

        }

        [Test]
        [TestCase()]
        public async Task PutCardsById()
        {
            //Arrange 
            string endpoint = "/api/cards/1";

            string cardsDataJson = $@"  
            {{
                ""name"": ""renamecards"",
               
            }}";

            // Set the Authorization header with client credentials
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken.AccessToken);

            //Act
            var url = _testEnvironmentUrl + endpoint;
            var response = await _httpClient.PutAsync(url, new StringContent(cardsDataJson, Encoding.UTF8, "application/json"));
            HttpResponseMessage getUpdatedCard = await _httpClient.GetAsync(url);
            string getUpdatedCardResponse = getUpdatedCard.Content.ReadAsStringAsync().Result;


            //Assert
            Assert.That(response.IsSuccessStatusCode, Is.True);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Content, Is.Not.Null);
            Assert.That(getUpdatedCardResponse, Does.Contain("renamecards"));

        }

        [Test]
        [TestCase()]
        public async Task DeleteCards()
        {
            //Arrange 
            string endpoint = "/api/cards/1";

            // Set the Authorization header with client credentials
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken.AccessToken);

            //Act
            var url = _testEnvironmentUrl + endpoint;
            var response = await _httpClient.DeleteAsync(url);
            HttpResponseMessage getUpdatedCard = await _httpClient.GetAsync(url);

            HttpResponseMessage getAllCards = await _httpClient.GetAsync(url);
            string allCardsContent = getAllCards.Content.ReadAsStringAsync().Result;


            //Assert
            Assert.That(response.IsSuccessStatusCode, Is.True);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(getUpdatedCard.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));

        }

        [Test]
        [TestCase()]
        public async Task GetCardBySetId()
        {
            //Arrange 
            string endpoint = "/api/Cards/cardsets/2";
            // Set the Authorization header with client credentials
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken.AccessToken);

            //Act
            var url = _testEnvironmentUrl + endpoint;
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            string responseContent = response.Content.ReadAsStringAsync().Result;

            //Assert
            Assert.That(response.IsSuccessStatusCode, Is.True);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Content, Is.Not.Null);
            Assert.That(responseContent, Does.Contain("id"));
            Assert.That(responseContent, Does.Contain("textQuestion"));
            Assert.That(responseContent, Does.Contain("logoUrl"));
            Assert.That(responseContent, Does.Contain("imageUrl"));
            Assert.That(responseContent, Does.Contain("theme"));

        }

        [Test]
        [TestCase()]
        public async Task GetCardByThemeId()
        {
            //Arrange 
            string endpoint = "/api/Cards/themes/{themeId}";
            // Set the Authorization header with client credentials
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken.AccessToken);

            //Act
            var url = _testEnvironmentUrl + endpoint;
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            string responseContent = response.Content.ReadAsStringAsync().Result;

            //Assert
            Assert.That(response.IsSuccessStatusCode, Is.True);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Content, Is.Not.Null);
            Assert.That(responseContent, Does.Contain("id"));
            Assert.That(responseContent, Does.Contain("textQuestion"));
            Assert.That(responseContent, Does.Contain("logoUrl"));
            Assert.That(responseContent, Does.Contain("imageUrl"));
            Assert.That(responseContent, Does.Contain("theme"));

        }
    }
}
