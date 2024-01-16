using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;

namespace api_gateway_tests.setting
{
    public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                var authenticationBuilder = services.AddAuthentication();
                authenticationBuilder.Services.Configure<AuthenticationOptions>(o =>
                {
                    o.SchemeMap.Clear();
                    ((IList<AuthenticationSchemeBuilder>)o.Schemes).Clear();
                });
                services
                    .AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                    .AddJwtBearer(x =>
                    {
                        x.MetadataAddress = "https://keycloak-api-gateway-test.apps.ocp5-inholland.joran-bergfeld.com/realms/spiegelspel/.well-known/openid-configuration";
                        x.SaveToken = true;

                        x.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = false,
                            ValidateAudience = true,
                            ValidAudience = "account",
                        };

                        x.RequireHttpsMetadata = false;
                    });

                    });
            builder.ConfigureAppConfiguration(configure => configure.AddJsonFile($"ocelot.dev.json"));

            builder.UseEnvironment("Development");

        }
    }
}
