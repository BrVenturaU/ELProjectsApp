using MediatR;

namespace ELProjectsApp.Shared.Abstractions.Messaging;

public interface IQuery<out TResponse>: IRequest<TResponse>
{
}
