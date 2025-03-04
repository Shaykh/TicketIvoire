using TicketIvoire.Administration.Domain.Utilisateurs;

namespace TicketIvoire.Administration.Domain.PropositionEvenements;

public interface IPropositionEvenementRepository
{
    Task<PropositionEvenement> GetByIdAsync(PropositionEvenementId id);
    Task<IEnumerable<PropositionEvenement>> GetAllAsync();
    Task<IEnumerable<PropositionEvenement>> GetAllByUtilisateurIdAsync(UtilisateurId utilisateurId);
    Task<IEnumerable<PropositionEvenement>> GetAllByStatutAsync(PropositionStatut statut);
    Task<IEnumerable<PropositionEvenement>> GetAllByDecisionAsync(PropositionDecision decision);
    Task<IEnumerable<PropositionEvenement>> GetAllByDateRangeAsync(DateTime dateDebut, DateTime dateFin);
    Task<IEnumerable<PropositionEvenement>> GetAllByLieuId(Guid LieuId);
}
