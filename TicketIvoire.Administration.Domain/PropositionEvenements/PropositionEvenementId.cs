using TicketIvoire.Shared.Domain;

namespace TicketIvoire.Administration.Domain.PropositionEvenements;

public record PropositionEvenementId(Guid Value) : TypedIdBase(Value);
