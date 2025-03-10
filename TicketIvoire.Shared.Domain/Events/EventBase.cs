namespace TicketIvoire.Shared.Domain.Events;

public record EventBase : IEvent
{
    public Guid EventId { get; init; } = Guid.NewGuid();

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}
