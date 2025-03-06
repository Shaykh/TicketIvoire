using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Shared.Infrastructure.Persistence;

public abstract class PersisterEventHandler<TDomainEvent> : IDomainEventHandler<TDomainEvent> where TDomainEvent : IDomainEvent
{
    public abstract Task HandleAsync(TDomainEvent tEvent, CancellationToken cancellationToken);
    public bool IsTransactional() => true;
}
