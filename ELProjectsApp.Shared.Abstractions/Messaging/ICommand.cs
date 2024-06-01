using MediatR;

namespace ELProjectsApp.Shared.Abstractions.Messaging;

public interface ICommand:IBaseCommand, IRequest
{
}

public interface ICommand<out TResponse>:IBaseCommand, IRequest<TResponse> { }

public interface IBaseCommand { }
