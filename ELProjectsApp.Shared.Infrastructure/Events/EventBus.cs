using ELProjectsApp.Shared.Abstractions.Events;
using ELProjectsApp.Shared.Infrastructure.Messaging;

namespace ELProjectsApp.Shared.Infrastructure.Events;

internal sealed class EventBus(InMemoryMessageQueue queue) : IEventBus
{
    public async Task PublishAsync<T>(
        T appEvent,
        CancellationToken cancellationToken = default)
        where T : class, IEvent
    {
        await queue.Writer.WriteAsync(appEvent, cancellationToken);
    }
}