using TicketIvoire.Shared.Domain.BusinessRules;
using TicketIvoire.Shared.Domain.Events;
using TicketIvoire.Shared.Domain.Exceptions;

namespace TicketIvoire.Shared.Domain;

public abstract class EntityBase
{
    public Guid Id { get; protected set; } = Guid.NewGuid();

    private readonly List<IDomainEvent> _events = [];
    public IReadOnlyCollection<IDomainEvent> Events => _events.AsReadOnly();

    protected void AddEvent(IDomainEvent domainEvent) => _events.Add(domainEvent);

    protected void AddEvents(IEnumerable<IDomainEvent> events) => _events.AddRange(events);

    protected void ClearEvents() => _events.Clear();

    protected static void CheckRule(IBusinessRule rule)
    {
        if (!rule.Validate())
        {
            throw new BrokenBusinessRuleException() { BrokenRule = rule };
        }
    }
}
