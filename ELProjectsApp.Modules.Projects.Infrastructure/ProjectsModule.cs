using ELProjectsApp.Modules.Projects.Application;
using ELProjectsApp.Modules.Projects.Application.Contracts;
using ELProjectsApp.Modules.Projects.Domain.Contracts;
using ELProjectsApp.Modules.Projects.Infrastructure.Database;
using ELProjectsApp.Modules.Projects.Infrastructure.Helpers;
using ELProjectsApp.Modules.Projects.Infrastructure.Providers;
using ELProjectsApp.Modules.Projects.Infrastructure.Repositories;
using ELProjectsApp.Shared.Abstractions.Data;
using ELProjectsApp.Shared.Abstractions.Modules;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ELProjectsApp.Modules.Projects.Infrastructure;

public class ProjectsModule : IModule
{
    public IServiceCollection AddModule(IServiceCollection services, IConfiguration configuration)
    {

        services.AddTransient<ITenantProvider, TenantProvider>();
        services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();  
        services.AddKeyedScoped<IUnitOfWork, ProjectsUnitOfWork>("ProjectsUnitOfWork");
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddDbContext<ProjectsDbContext>((sp, options) =>
            {
                var tenantProvider = sp.GetRequiredService<ITenantProvider>();
                var connectionString = tenantProvider.GetTenantConnectionString();
                options.UseSqlServer(connectionString, options =>
                {

                    options.MigrationsAssembly(typeof(ProjectsModule).Assembly.GetName().Name);
                });
            }
        );
        services.AddMediatR(conf =>
        {
            conf.RegisterServicesFromAssemblies(
                Assembly.GetAssembly(typeof(ProjectsApplicationAssemblyReference))
            );
        });
        return services;
    }
}
