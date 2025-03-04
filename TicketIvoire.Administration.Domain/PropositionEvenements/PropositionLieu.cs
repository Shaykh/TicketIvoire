namespace TicketIvoire.Administration.Domain.PropositionEvenements;

public record PropositionLieu(string? Nom, string? Description, string? Ville, Guid? LieuEvenementId)
{
}
