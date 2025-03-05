using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Administration.Domain.LieuEvenements.Events;

public record LieuAjouteEvent(Guid LieuId, uint? Capacite, string Nom, string Description, string Adresse, string Ville) : DomainEventBase;
