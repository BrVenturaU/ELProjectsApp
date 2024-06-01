using ELProjectsApp.Modules.Organizations.Api;
using ELProjectsApp.Modules.Organizations.Application;
using ELProjectsApp.Modules.Organizations.Domain.Contracts;
using ELProjectsApp.Modules.Organizations.Infrastructure.Api;
using ELProjectsApp.Modules.Organizations.Infrastructure.Database;
using ELProjectsApp.Modules.Organizations.Infrastructure.Repositories;
using ELProjectsApp.Shared.Abstractions.Data;
using ELProjectsApp.Shared.Abstractions.Modules;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ELProjectsApp.Modules.Organizations.Infrastructure;

public class OrganizationsModule : IModule
{
    public IServiceCollection AddModule(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IOrganizationsApi, OrganizationsApi>();
        services.AddKeyedScoped<IUnitOfWork, OrganizationsUnitOfWork>("OrganizationsUnitOfWork");
        services.AddScoped<IOrganizationRepository, OrganizationRepository>();
        services.AddDbContext<OrganizationsDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Master"), options =>
            {
                options.MigrationsHistoryTable("__OrganizationsMigrationsHistory", "Business");
                options.MigrationsAssembly(typeof(OrganizationsModule).Assembly.GetName().Name);
            })
        );
        services.AddMediatR(conf =>
        {
            conf.RegisterServicesFromAssemblies(
                Assembly.GetAssembly(typeof(OrganizationsApplicationAssemblyReference))
            );
        });
        return services;
    }
}
