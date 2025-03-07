namespace TicketIvoire.Shared.Domain.Events;

public record EventBase : IEvent
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}
