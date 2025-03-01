using MediatR;

namespace TicketIvoire.Shared.Application.Commands;

public interface ICommand : IRequest
{
}

public interface ICommand<out TResult> : IRequest<TResult>
{
}
