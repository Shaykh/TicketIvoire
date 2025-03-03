using TicketIvoire.Shared.Domain.BusinessRules;
using TicketIvoire.Shared.Domain.Exceptions;

namespace TicketIvoire.Shared.Domain;

public abstract class EntityBase
{
    public Guid Id { get; protected set; } = Guid.NewGuid();

    protected static void CheckRule(IBusinessRule rule)
    {
        if (!rule.Validate())
        {
            throw new BrokenBusinessRuleException() { BrokenRule = rule };
        }
    }
}
