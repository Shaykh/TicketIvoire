using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Administration.Domain.PropositionEvenements.Events;
public record PropositionEvenementVerifieeEvent(Guid PropositionEvenementId) : DomainEventBase
{
}
