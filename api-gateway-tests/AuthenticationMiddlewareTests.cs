using Moq;
using Moq.Protected;
using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Newtonsoft.Json.Linq;
using Microsoft.IdentityModel.Tokens;
using Keycloak.Net;
using Keycloak.AuthServices.Sdk.Admin;
using Keycloak.AuthServices.Sdk.Admin.Models;
using KeycloakApiClient.Models;
using Keycloak.Net.Models.Users;
using Keycloak.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Keycloak.Net.Models.Clients;
using Microsoft.Identity.Client;
using Ocelot.DependencyInjection;
using Ocelot.Requester;
using Microsoft.AspNetCore.Authorization;

namespace api_gateway_tests
{
    public class AuthenticationMiddlewareTests
    {
        private Mock<IServiceCollection> _services;
        private Mock<IKeycloakClient> _mockKeycloakClient;
        private readonly Mock<IOptions<JwtBearerOptions>> mockJWTOptions;
        private readonly Mock<IHttpClientFactory> mockHttpClientFactory;
        private  Mock<IOcelotBuilder> _mockOcelotBuilder;
        private IHttpRequester _httpRequester;
        public AuthenticationMiddlewareTests()
        {
            _mockKeycloakClient = new Mock<IKeycloakClient>();
        }
        [Test]
        public async Task RegisteringOfANewUser_ShouldCreateUserInKeycloak()
        {
            ////Arrange
            ////Moq Httpclient
            

            _mockKeycloakClient

                  .Setup(x => x.GetUser("playing-card", "53443927-e88b-487c-b23f-94fe0126f8cf"))
              .ReturnsAsync(new Keycloak.AuthServices.Sdk.Admin.Models.User()
              {
                  Id = "53443927-e88b-487c-b23f-94fe0126f8cf",
                  FirstName = "Peter",
                  LastName = "Harrington",
                  Email = "peterharrinton@outlook.com",
                  RealmRoles = new[] { "user" }
              });

            var userInfo = await _mockKeycloakClient.Object.GetUser("playing-card", "53443927-e88b-487c-b23f-94fe0126f8cf");

            // Assert that the returned UserInfo object is correct
            Assert.AreEqual("peterharrinton@outlook.com", userInfo.Email);
            Assert.AreEqual(new[] { "user" }, userInfo.RealmRoles);



        }

        [Test]
        public async Task OnlyUserWithUserRoleUserShouldBeAuthenticated()
        {
            var mockJwtBearerHandler = new Mock<JwtBearerHandler>();
            var mockKeycloakClient = new Mock<IKeycloakClient>();
            var mockToken = new Mock<AccessToken>();
            
            mockJWTOptions.Setup(x => x.Value).Returns(new JwtBearerOptions
            {
                SaveToken = true,
               //MetadataAddress = builder.Configuration.GetValue<string>("Configuration:MetadataAddress"),

                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = true,
                    ValidAudience = "account",
                }


            });

            //var handler = new Mock<HttpMessageHandler>();
            //handler.Protected()
            //    .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            //    .ReturnsAsync(new HttpResponseMessage
            //    {
            //        StatusCode = HttpStatusCode.Created,


            //    });
            // var httpClient = new HttpClient(handler.Object)
            ////mockToken.Setup(x => x.).Returns("my-jwt-token");

            ////mockHttpClientFactory.Setup(x => x.CreateHttpClient()).Returns(new HttpClient());
            ////IOcelotBuilder builder;
            //_mockOcelotBuilder = new Mock<IOcelotBuilder>();
            //_mockOcelotBuilder.Setup(x => x.Services.AddAuthentication("Bearer"));
            //_httpRequester.GetResponse(httpClient.)
            //var request = _httpRequester.
            //{

            //};
            //var response = await _mockOcelotBuilder.ProcessAsync(request);

            //Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }


        [SetUp]
        public async Task SetupAsync()
        {
            //var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            //mockHttpMessageHandler
            //    .Protected()
            //    .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            //    .ReturnsAsync(new HttpResponseMessage
            //    {
            //        StatusCode = HttpStatusCode.OK,
            //        Content = new StringContent("{\"issuer\":\"https://keycloak-keycloak.apps.ocp5-inholland.joran-bergfeld.com/realms/master/\"}") // Mocked response
            //    });

            //var httpClient = new HttpClient(mockHttpMessageHandler.Object);


        }

       
    }
}