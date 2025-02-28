namespace TicketIvoire.Shared.Domain.Events;

public interface IEventPublisher
{
    Task PublishAsync<TEvent>(TEvent domainEvent, CancellationToken token) where TEvent : IEvent;
}
