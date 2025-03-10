namespace TicketIvoire.Shared.Domain.Events;

public interface IEvent
{
    Guid EventId { get; }
    DateTime CreatedAt { get; }
}
