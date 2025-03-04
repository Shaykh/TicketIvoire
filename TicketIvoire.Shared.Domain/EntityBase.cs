using TicketIvoire.Shared.Domain.BusinessRules;
using TicketIvoire.Shared.Domain.Events;
using TicketIvoire.Shared.Domain.Exceptions;

namespace TicketIvoire.Shared.Domain;

public abstract class EntityBase
{
    public static void CheckRule(IBusinessRule rule)
    {
        if (!rule.Validate())
        {
            throw new BrokenBusinessRuleException(rule);
        }
    }

    private readonly List<IDomainEvent> _domainEvents = [];
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearEvents() => _domainEvents.Clear();

    public void RegisterEvent(IDomainEvent domainEvent)
    {
        ArgumentNullException.ThrowIfNull(domainEvent);

        _domainEvents.Add(domainEvent);
    }
}
