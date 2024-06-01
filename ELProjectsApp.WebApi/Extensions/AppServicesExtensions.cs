using ELProjectsApp.Modules.Organizations.Presentation;
using ELProjectsApp.Modules.Projects.Presentation;
using ELProjectsApp.Modules.Projects.Presentation.Filters;
using ELProjectsApp.Modules.Users.Presentation;
using ELProjectsApp.Shared.Abstractions.Modules;
using ELProjectsApp.WebApi.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace ELProjectsApp.WebApi.Extensions;

public static class AppServicesExtensions
{
    public static WebApplicationBuilder AddAppModule(this WebApplicationBuilder builder, IModule module)
    {
        module.AddModule(builder.Services, builder.Configuration);
        return builder;
    }

    public static IServiceCollection AddFilters(this IServiceCollection services)
    {
        services.AddTransient<TenantAuthorizationFilter>();
        return services;
    }

    public static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration configuration)
    {

        var jwtSettings = configuration.GetSection("JwtSettings");

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["ValidIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"])),
                ClockSkew = TimeSpan.Zero,
            };
        });

        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "ELProjectsApi",
                Version = "v1",
                Contact = new OpenApiContact
                {
                    Name = "Brandon Ventura",
                    Email = "brandon@gmail.com",
                    Url = new Uri("https://bventura.netlify.app")
                }
            });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Use JWT token with Bearer authentication",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            options.OperationFilter<SwaggerSecurityRequirementsOperationFilter>();

            var xmlFile = $"{Assembly.GetAssembly(typeof(UsersPresentationAssemblyReference)).GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);

            xmlFile = $"{Assembly.GetAssembly(typeof(OrganizationsPresentationAssemblyReference)).GetName().Name}.xml";
            xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);

            xmlFile = $"{Assembly.GetAssembly(typeof(ProjectsPresentationAssemblyReference)).GetName().Name}.xml";
            xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        });

        return services;
    }
}
