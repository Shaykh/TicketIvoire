using TicketIvoire.Shared.Domain.BusinessRules;
using TicketIvoire.Shared.Domain.Events;
using TicketIvoire.Shared.Domain.Exceptions;

namespace TicketIvoire.Shared.Domain;

public abstract class EntityBase
{
    public Guid Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    public DateTime UpdatedAt { get; protected set; }
    public bool IsDeleted() => DeletedAt.HasValue;
    public DateTime? DeletedAt { get; protected set; }
    public Guid? DeletedBy { get; protected set; }
    public Guid? UpdatedBy { get; protected set; }
    public Guid? CreatedBy { get; protected set; }

    private readonly List<IEvent> _events = [];
    public IReadOnlyCollection<IEvent> Events => _events.AsReadOnly();

    protected EntityBase()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Delete(Guid deletedBy)
    {
        DeletedAt = DateTime.UtcNow;
        DeletedBy = deletedBy;
    }

    public void Update(Guid updatedBy)
    {
        UpdatedAt = DateTime.UtcNow;
        UpdatedBy = updatedBy;
    }

    protected void AddEvent(IEvent @event) => _events.Add(@event);

    protected void ClearEvents() => _events.Clear();

    protected void AddEvents(IEnumerable<IEvent> events) => _events.AddRange(events);

    protected static void CheckRule(IBusinessRule rule)
    {
        if (!rule.Validate())
        {
            throw new BrokenBusinessRuleException(rule) { BrokenRule = rule };
        }
    }
}
