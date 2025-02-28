namespace TicketIvoire.Shared.Domain.Events;

public class EventBase : IEvent
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    public IDictionary<string, object> MetaData { get; init; } = new Dictionary<string, object>();
}
