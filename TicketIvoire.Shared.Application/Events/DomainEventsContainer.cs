using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Shared.Application.Events;

public class DomainEventsContainer : IDomainEventsContainer
{
    private readonly List<IDomainEvent> _domainEvents = [];
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearEvents() => _domainEvents.Clear();

    public void RegisterEvent(IDomainEvent domainEvent)
    {
        ArgumentNullException.ThrowIfNull(domainEvent);

        _domainEvents.Add(domainEvent);
    }
}
