namespace TicketIvoire.Shared.Domain.Events;

public interface IDomainEventsContainer
{
    void AddEvents(IEnumerable<IDomainEvent> domainEvents);
    void RegisterEvent(IDomainEvent domainEvent);
    IReadOnlyList<IDomainEvent> DomainEvents { get; }
    void ClearEvents();
}
