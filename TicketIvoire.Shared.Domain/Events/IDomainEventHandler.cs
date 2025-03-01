namespace TicketIvoire.Shared.Domain.Events;

public interface IDomainEventHandler<in TEvent> where TEvent : IDomainEvent
{
    Task HandleAsync(TEvent tEvent, CancellationToken cancellationToken);
    bool IsTransactional();
}
