using MediatR;

namespace ELProjectsApp.Shared.Abstractions.Events;

public interface IEventHandler<in TEvent>: INotificationHandler<TEvent>
    where TEvent : IEvent
{
}
