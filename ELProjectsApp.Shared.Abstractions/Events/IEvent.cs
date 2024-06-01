using MediatR;

namespace ELProjectsApp.Shared.Abstractions.Events;

public interface IEvent : INotification
{
    Guid Id { get; init; }
}

public abstract record Event(Guid Id) : IEvent;