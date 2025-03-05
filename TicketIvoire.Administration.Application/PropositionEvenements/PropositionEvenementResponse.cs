namespace TicketIvoire.Administration.Application.PropositionEvenements;

public record PropositionEvenementResponse(Guid PropositionEvenementId,
    string Nom,
    string Description,
    DateTime DateDebut,
    DateTime DateFin,
    Guid? LieuId,
    string? LieuNom,
    EnumDto Statut,
    PropositionDecisionDto? DerniereDecision,
    Guid UtilisateurId
    );
