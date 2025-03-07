using TicketIvoire.Shared.Domain;

namespace TicketIvoire.Administration.Domain.Membres;

public record MembreId(Guid Value) : TypedIdBase(Value);
