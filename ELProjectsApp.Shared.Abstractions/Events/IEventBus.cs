namespace ELProjectsApp.Shared.Abstractions.Events;

public interface IEventBus
{
    Task PublishAsync<T>(
        T appEvent,
        CancellationToken cancellationToken = default)
        where T : class, IEvent;
}
