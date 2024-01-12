using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WireMock.Server;

namespace api_gateway_tests
{
    public class IntegrationTestAppFactory<TProgram> 
        : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var wiremockServer = WireMockServer.Start(9876);

            builder.ConfigureAppConfiguration(configurationBuilder =>
            {
                configurationBuilder.AddInMemoryCollection(new KeyValuePair<string, string>[]
                   {
                   new ("Configuration:AuthorizationUrl", wiremockServer.Urls[0])});
            })
                .ConfigureServices(collection => collection.AddSingleton(wiremockServer));
            
        }
    }
}
