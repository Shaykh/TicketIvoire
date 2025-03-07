using TicketIvoire.Shared.Domain;

namespace TicketIvoire.Administration.Domain.LieuEvenements;

public record LieuId(Guid Value) : TypedIdBase(Value);
