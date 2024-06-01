using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ELProjectsApp.Shared.Abstractions.Modules;

public interface IModule
{
    IServiceCollection AddModule(IServiceCollection services, IConfiguration configuration);
}
