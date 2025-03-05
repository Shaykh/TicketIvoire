using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Administration.Domain.LieuEvenements.Events;
public record LieuCoordonneesGeographiquesDefiniesEvent(Guid LieuId, decimal Latitude, decimal Longitude) : DomainEventBase;
