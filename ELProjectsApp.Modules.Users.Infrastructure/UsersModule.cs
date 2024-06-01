using ELProjectsApp.Modules.Users.Api;
using ELProjectsApp.Modules.Users.Application;
using ELProjectsApp.Modules.Users.Application.Contracts;
using ELProjectsApp.Modules.Users.Infrastructure.Api;
using ELProjectsApp.Modules.Users.Infrastructure.Database;
using ELProjectsApp.Modules.Users.Infrastructure.Providers;
using ELProjectsApp.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ELProjectsApp.Modules.Users.Infrastructure;

public class UsersModule: IModule
{
    public IServiceCollection AddModule(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IUsersApi, UsersApi>();
        services.AddScoped<IAuthProvider, AuthProvider>();
        services.AddMediatR(conf =>
        {
            conf.RegisterServicesFromAssemblies(
                Assembly.GetAssembly(typeof(UsersApplicationAssemblyReference))
            );
        });

        services.AddDbContext<UsersDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Master"), options =>
            {
                options.MigrationsHistoryTable("__UsersMigrationsHistory", "Security");
                options.MigrationsAssembly(typeof(UsersModule).Assembly.GetName().Name);
            })
        );

        services.AddIdentity<IdentityUser<Guid>, IdentityRole<Guid>>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 10;
            options.User.RequireUniqueEmail = true;
        }).AddEntityFrameworkStores<UsersDbContext>()
        .AddDefaultTokenProviders();

        return services;
    }
}
