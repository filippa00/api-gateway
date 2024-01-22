using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Extentions;
using Prometheus;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args)
    ;
        builder.WebHost.ConfigureKestrel(serverOptions =>
        {
            serverOptions.ListenAnyIP(8080); // Listen for HTTP on port 8080
        });


        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddOcelot();
        builder.Services.DecorateClaimAuthoriser();

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gateway v1.0", Version = "v1" });
            c.AddSecurityDefinition("OAuth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    Implicit = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri($"{Environment.GetEnvironmentVariable("AuthorizationUrl")}"),
                    }
                }
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement{
        {
        new OpenApiSecurityScheme{
            Reference = new OpenApiReference{
                Type = ReferenceType.SecurityScheme,
                Id = "OAuth2"
            }
        },
        new string[] {}
            }

        });
        });
        builder.Services.AddSwaggerForOcelot(builder.Configuration);
        builder.WebHost.ConfigureAppConfiguration(configure => configure.AddJsonFile(Environment.GetEnvironmentVariable("ocelotLocation").ToString()));
        builder.Logging.AddConsole();
        builder.Services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.MetadataAddress = $"{Environment.GetEnvironmentVariable("MetadataAddress")}";

                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = true,
                    ValidAudience = "account",
                };

                x.RequireHttpsMetadata = false;
            });

        builder.Services.AddAuthorization(o =>
        {
            o.DefaultPolicy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();
        });
        builder.Services.AddScoped<IClaimsTransformation, CustomClaimsTransformer>();
        var app = builder.Build();
        app.UseHttpsRedirection();


        app.UsePathBase("/gateway");

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }



        app.MapControllers();
        app.UseSwaggerForOcelotUI(opt =>
        {
            opt.DownstreamSwaggerEndPointBasePath = "/gateway/swagger/docs";
            opt.PathToSwaggerGenerator = "/swagger/docs";
        });
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseMetricServer();
        app.UseOcelot().Wait();
        app.Run();
    }
}