using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Administration.Domain.Membres.Events;
public record MembreReactiveEvent(Guid MembreId) : DomainEventBase
{
}
