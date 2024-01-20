using api_gateway_tests.setting;
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

namespace api_gateway_tests
{
    public  class CardSetsTests : BaseTestClass
    {
        [SetUp]
        public void AdminLogin()
        {
            _adminToken = GetAdminToken();
        }

        [Test]
        public async Task GetCardSets()
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
    }
}

