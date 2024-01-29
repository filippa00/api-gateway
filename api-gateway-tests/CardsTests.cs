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
        public async Task GetCards_ShouldReturnCards_WithStatusOk()
        {
            //Arrange
            string endpoint = "/api/cards";
            //Set the Authorization header with client credentials
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken.AccessToken);

            //Act
            var url = _testEnvironmentUrl + endpoint;
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            string responseContent = response.Content.ReadAsStringAsync().Result;

            // Assert
            Assert.That(response.IsSuccessStatusCode, Is.True);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Content, Is.Not.Null);
            Assert.That(responseContent, Does.Contain("id"));
            Assert.That(responseContent, Does.Contain("textQuestion"));
            Assert.That(responseContent, Does.Contain("imageUrl"));
            Assert.That(responseContent, Does.Contain("theme"));

        }



        [Test]
        [TestCase("26d30852-9a3b-4270-b4e4-3d68a2bab32d")]
        public async Task GetCardsById_ShouldReturnCard_WithStatusOk(string id)
        {
            //Arrange
            string endpoint = "/api/cards/";

            //Set the Authorization header with client credentials
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
            Assert.That(responseContent, Does.Contain("textQuestion"));
            Assert.That(responseContent, Does.Contain("imageUrl"));
            Assert.That(responseContent, Does.Contain("theme"));

        }

      
        [TestCase("67219375-5227-4df8-9a5e-117f9927dc7e")]
        public async Task GetCardBySetId_ShouldReturnACardset_WithStatusOk(string id)
        {
            //Arrange
            string endpoint = "/api/Cards/cardsets/";
            //Set the Authorization header with client credentials
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken.AccessToken);

            // Act
            var url = _testEnvironmentUrl + endpoint + id;
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            string responseContent = response.Content.ReadAsStringAsync().Result;

            //Assert
            Assert.That(response.IsSuccessStatusCode, Is.True);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Content, Is.Not.Null);
            Assert.That(responseContent, Does.Contain("id"));
            Assert.That(responseContent, Does.Contain("textQuestion"));
            Assert.That(responseContent, Does.Contain("imageUrl"));
            Assert.That(responseContent, Does.Contain("theme"));

        }

        [Test]
        [TestCase("10e261ef-3050-44e9-8a98-269af835189c")]
        public async Task GetCardByThemeId(string id)
        {
            //Arrange
            string endpoint = "/api/Cards/themes/";
            //Set the Authorization header with client credentials
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
            Assert.That(responseContent, Does.Contain("textQuestion"));
            Assert.That(responseContent, Does.Contain("imageUrl"));
            Assert.That(responseContent, Does.Contain("theme"));

        }
    }
}
