using TicketIvoire.Administration.Domain.Utilisateurs;

namespace TicketIvoire.Administration.Domain.PropositionEvenements;

public record PropositionDecision(UtilisateurId UtilisateurId, DateTime DateDecision, string Code, string? Raisons)
{
    public static string AccepterCode => "ACCEPTER";
    public static string RefuserCode => "REFUSER";
}
