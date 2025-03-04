using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Administration.Domain.PropositionEvenements.Events;
public record PropositionEvenementAccepteeEvent(Guid PropositionEvenement, Guid UtilisateurId, DateTime DateDecision) : DomainEventBase
{
}
