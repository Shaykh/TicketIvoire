namespace TicketIvoire.Shared.Domain.Events;

public interface IEvent
{
    Guid Id { get; }
    DateTime CreatedAt { get; }
    IDictionary<string, object> MetaData { get; }
}
