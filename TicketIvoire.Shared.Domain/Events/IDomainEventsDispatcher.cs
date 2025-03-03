namespace TicketIvoire.Shared.Domain.Events;

public interface IDomainEventsDispatcher
{
    Task DispatchAllTransactionalEventsAsync(IReadOnlyList<IDomainEvent> domainEvents, CancellationToken cancellationToken);

    Task DispatchAllNoTransactionalEventsAsync(IReadOnlyList<IDomainEvent> domainEvents, CancellationToken cancellationToken);
}
