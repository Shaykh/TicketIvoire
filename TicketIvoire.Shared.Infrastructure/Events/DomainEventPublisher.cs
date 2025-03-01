using Microsoft.Extensions.DependencyInjection;
using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Shared.Infrastructure.Events;

public class DomainEventPublisher(IServiceProvider serviceProvider) : IDomainEventPublisher
{
    private readonly Type _domainEventsHandlerType = typeof(IDomainEventHandler<>);

    public async Task PublishAsync<TEvent>(TEvent domainEvent, CancellationToken cancellationToken) where TEvent : IDomainEvent
    {
        IEnumerable<dynamic> handlers = GetHandlers(domainEvent);
        foreach (dynamic handler in handlers)
        {
            await InvokeHandleAsync(handler, domainEvent, cancellationToken);
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
