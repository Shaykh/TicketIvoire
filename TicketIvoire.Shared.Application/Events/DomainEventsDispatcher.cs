using Microsoft.Extensions.DependencyInjection;
using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Shared.Application.Events;

public class DomainEventsDispatcher(IServiceProvider serviceProvider) : IDomainEventsDispatcher
{
    private readonly Type _domainEventsHandlerType = typeof(IDomainEventHandler<>);

    public async Task DispatchAllNoTransactionalEventsAsync(IReadOnlyList<IDomainEvent> domainEvents, CancellationToken cancellationToken)
    {
        foreach (IDomainEvent domainEvent in domainEvents)
        {
            IEnumerable<dynamic> handlers = GetHandlers(domainEvent);
            foreach (dynamic handler in handlers.Where(d => !d.IsTransactional()))
            {
                await InvokeHandleAsync(handler, domainEvent, cancellationToken);
            }
        }
    }

    public async Task DispatchAllTransactionalEventsAsync(IReadOnlyList<IDomainEvent> domainEvents, CancellationToken cancellationToken)
    {
        foreach (IDomainEvent domainEvent in domainEvents)
        {
            IEnumerable<dynamic> handlers = GetHandlers(domainEvent);
            foreach (dynamic handler in handlers.Where(d => d.IsTransactional()))
            {
                await InvokeHandleAsync(handler, domainEvent, cancellationToken);
            }
        }
    }

    private static async Task InvokeHandleAsync(dynamic handler, IDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var task = (Task)handler.GetType().GetMethod(nameof(IDomainEventHandler<IDomainEvent>.HandleAsync))!.Invoke(handler, new object[] { domainEvent, cancellationToken });
        await task.ConfigureAwait(false);
    }

    private IEnumerable<dynamic> GetHandlers(IDomainEvent domainEvent)
        => serviceProvider.GetServices(_domainEventsHandlerType.MakeGenericType(domainEvent.GetType())).Cast<dynamic>();
}
