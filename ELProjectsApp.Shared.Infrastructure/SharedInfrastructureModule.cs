using ELProjectsApp.Shared.Abstractions.Authentication;
using ELProjectsApp.Shared.Abstractions.Events;
using ELProjectsApp.Shared.Abstractions.Modules;
using ELProjectsApp.Shared.Infrastructure.Authentication;
using ELProjectsApp.Shared.Infrastructure.Events;
using ELProjectsApp.Shared.Infrastructure.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ELProjectsApp.Shared.Infrastructure;

public class SharedInfrastructureModule : IModule
{
    public IServiceCollection AddModule(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHostedService<EventProcessorJob>();
        services.AddSingleton<IEventBus, EventBus>();
        services.AddSingleton<InMemoryMessageQueue>();
        services.AddScoped<IUserContext, WebUserContext>();
        return services;
    }
}
