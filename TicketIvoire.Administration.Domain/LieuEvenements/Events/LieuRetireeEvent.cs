using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Administration.Domain.LieuEvenements.Events;

public record LieuRetireeEvent(Guid LieuId) : DomainEventBase;
