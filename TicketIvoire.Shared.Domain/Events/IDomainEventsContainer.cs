namespace TicketIvoire.Shared.Domain.Events;

public interface IDomainEventsContainer
{
    void RegisterEvent(IDomainEvent domainEvent);
    IReadOnlyList<IDomainEvent> DomainEvents { get; }
    void ClearEvents();
}
