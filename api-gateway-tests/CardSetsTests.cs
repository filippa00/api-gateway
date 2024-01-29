using Microsoft.AspNetCore.Mvc.Testing;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Wmhelp.XPath2.MS;

namespace api_gateway_tests
{
    public class CardSetsTests : BaseTestClass
    {
        [SetUp]
        public void AdminLogin()
        {
            _adminToken = GetAdminToken();
        }

        [Test]
        public async Task GetCardSets_ShouldReturnCardSets_WithStatusOk()
        {
            //Arrange 
            string endpoint = "/api/CardSets";
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
            Assert.That(responseContent, Does.Contain("name"));
            Assert.That(responseContent, Does.Contain("description"));
            Assert.That(responseContent, Does.Contain("cards"));
        }

        [Test]
        public async Task PostToCardsSets_ShouldReturnNewCardSet_WithStatusCreated()
        {
            //Arrange 
            string endpoint = "/api/CardSets";



            int nameNr = _random.Next(0, 1000);
            int descriptionNr = _random.Next(0, 1000);

            // Cards data to be sent in the request body
            string cardsDataJson = $@"  
            {{
                  ""id"": ""{Guid.NewGuid()}"",     
                ""name"": ""New Cardset from api gateway integration test {nameNr}"",
                ""description"": ""Description for the cardsetfrom api gateway integration test {descriptionNr}"",
                ""logoUrl"": """"
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
            Assert.That(responseContent, Does.Contain($"New Cardset from api gateway integration test {nameNr}"));
            Assert.That(responseContent, Does.Contain($"Description for the cardsetfrom api gateway integration test {descriptionNr}"));
        }

        [TestCase("67219375-5227-4df8-9a5e-117f9927dc7e")]
        public async Task GetCardSetsById_ShouldReturnACardSet_WithStatusOk(string id)
        {
            //Arrange 
            string endpoint = "/api/CardSets/";
            // Set the Authorization header with client credentials
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken.AccessToken);

            //Act
            var url = _testEnvironmentUrl + endpoint + id;
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            string responseContent = response.Content.ReadAsStringAsync().Result;

            //Assert
            Assert.That(response.IsSuccessStatusCode, Is.True);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Content, Is.Not.Null);
            Assert.That(responseContent, Does.Contain("id"));
            Assert.That(responseContent, Does.Contain("name"));
            Assert.That(responseContent, Does.Contain("description"));
            Assert.That(responseContent, Does.Contain("cards"));
        }
    }
}

