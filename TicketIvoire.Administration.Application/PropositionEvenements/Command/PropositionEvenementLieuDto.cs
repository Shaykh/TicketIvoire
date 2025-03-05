namespace TicketIvoire.Administration.Application.PropositionEvenements.Command;

public record PropositionEvenementLieuDto(string? Nom, string? Description, string? Ville, Guid? LieuEvenementId);
