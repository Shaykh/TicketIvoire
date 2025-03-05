using TicketIvoire.Administration.Domain.Utilisateurs;

namespace TicketIvoire.Administration.Application.PropositionEvenements;

public record PropositionDecisionDto(UtilisateurId UtilisateurId, 
    DateTime DateDecision, 
    string Code, 
    string? Raisons);
