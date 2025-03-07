using TicketIvoire.Administration.Domain.Utilisateurs;

namespace TicketIvoire.Administration.Domain.PropositionEvenements;

public record PropositionDecision(UtilisateurId UtilisateurId, DateTime DateDecision, string Code, string? Raisons)
{
    public static string AccepterCode => "ACCEPTER";
    public static string RefuserCode => "REFUSER";

    public static PropositionDecision PropositionAcceptee(Guid utilisateurId, DateTime dateDecision) => new (
        new UtilisateurId(utilisateurId),
        dateDecision,
        AccepterCode,
        null);

    public static PropositionDecision PropositionRefusee(Guid utilisateurId, DateTime dateDecision, string raisons) => new(
        new UtilisateurId(utilisateurId),
        dateDecision,
        RefuserCode,
        raisons);
}
