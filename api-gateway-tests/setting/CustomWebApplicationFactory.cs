using Extentions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;

namespace api_gateway_tests.setting
{
    public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                //services.AddOcelot();
                //services.DecorateClaimAuthoriser(); 
                //services.AddScoped<IClaimsTransformation, CustomClaimsTransformer>();
            });
            builder.ConfigureAppConfiguration(configure => configure.AddJsonFile($"ocelot.dev.json"));

            builder.UseEnvironment("Development");
           
        }
    }
}
