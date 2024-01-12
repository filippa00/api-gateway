using Keycloak.Net.Models.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace api_gateway_tests
{
    public class AuthenticationIntegrationTests
    {
        private WireMockServer _wireMockServer;
        private HttpClient _httpClient;
       

        [SetUp]
        public void StartServer()
        {
            //Starting a mock server 
            _wireMockServer = WireMockServer.Start(9876);
            _httpClient = new HttpClient { BaseAddress = new Uri(_wireMockServer.Urls[0]) };
        }
        //Happy flows 
        //Register a user with a role
        //Login a user

        //Sad flows 
        //unauthorized 
        //forbidden 

        [Test]
        public void SuccessfulRegistration_UserRegistrationEndpoint_WireMock()
        {

            // Set up WireMock response for the downstream service (Keycloak)
            _wireMockServer
            .Given(Request.Create().WithPath("/admin/realms/master/users").UsingPost())
            
            
            .RespondWith(Response.Create().WithStatusCode(200).WithHeader("Content-Type", "application/json"));
            //.WithBody(@"{'username': 'ranley@example.com','email': 'ranley@example.com','enabled': true,'firstName': 'Ranley','lastName': 'Lonley','credentials': [{'type': 'password','value': '1234' }]}")); 
            // Act
            var result = _httpClient.PostAsync("/admin/realms/master/users", new StringContent(@"{'username': 'ranley@example.com','email': 'ranley@example.com','enabled': true,'firstName': 'Ranley','lastName': 'Lonley','credentials': [{'type': 'password','value': '1234' }]}")).Result;
            
            // Assert
            Assert.AreEqual(200, (int)result.StatusCode);
        }

        [Test]
        public void SuccessfulRegistration_UserRegistrationEndpoint_WireMock2()
        {
          _wireMockServer
            .Given(Request.Create()
            .WithPath("/api/users")
            .UsingPost())
            .RespondWith(Response.Create()
            .WithStatusCode(200)
            .WithBody(@"{'username': 'ranley@example.com','email': 'ranley@example.com','enabled': true,'firstName': 'Ranley','lastName': 'Lonley','credentials': [{'type': 'password','value': '1234' }]}"));


            var result = _httpClient.PostAsync("http://localhost:9876/api/users", new StringContent("{}")).Result;

            // Assert
            Assert.AreEqual(200, (int)result.StatusCode);
        }



            [TearDown]
        public void StopServer()
        {
            // This stops the mock server to clean up after ourselves
            _wireMockServer.Stop();
            _wireMockServer.Dispose();
        }
    }
}
