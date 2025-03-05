using TicketIvoire.Shared.Domain.Events;

namespace TicketIvoire.Administration.Domain.LieuEvenements.Events;

public record LieuRetireEvent(Guid LieuId, string Raisons) : DomainEventBase;
