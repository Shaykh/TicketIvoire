using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Administration.Domain.Membres.Events;

public record MembreCreeEvent(Guid MemberId, string Login, string Email, string Nom, string Prenom, string Telephone, DateTime DateAdhesion) : DomainEventBase
{
}
