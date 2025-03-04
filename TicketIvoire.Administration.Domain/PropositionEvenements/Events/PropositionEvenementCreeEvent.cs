using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Administration.Domain.PropositionEvenements.Events;
public record PropositionEvenementCreeEvent(Guid PropositionEvenementId, Guid UtilisateurId, string Nom, string Description, DateTime DateDebut, DateTime DateFin, PropositionLieu Lieu) : DomainEventBase
{
}
