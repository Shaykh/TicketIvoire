using TicketIvoire.Shared.Domain;

namespace TicketIvoire.Administration.Domain.Utilisateurs;

public record UtilisateurId(Guid Value) : TypedIdBase(Value);
