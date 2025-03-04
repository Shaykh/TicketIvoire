using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Administration.Domain.PropositionEvenements.Events;

public record PropositionEvenementRefuseeEvent(Guid PropositionEvenement, Guid UtilisateurId, DateTime DateDecision, string Raisons) : DomainEventBase
{
}
