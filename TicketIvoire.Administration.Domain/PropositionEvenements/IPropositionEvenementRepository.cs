using TicketIvoire.Administration.Domain.Utilisateurs;

namespace TicketIvoire.Administration.Domain.PropositionEvenements;

public interface IPropositionEvenementRepository
{
    Task<PropositionEvenement> GetByIdAsync(PropositionEvenementId id);
    Task<IEnumerable<PropositionEvenement>> GetAllAsync(uint? pageNumber, uint? numberByPage);
    Task<IEnumerable<PropositionEvenement>> GetAllByStatutAsync(PropositionStatut statut, uint? pageNumber, uint? numberByPage);
    Task<IEnumerable<PropositionEvenement>> GetAllByDecisionAsync(PropositionDecision decision, uint? pageNumber, uint? numberByPage);
    Task<IEnumerable<PropositionEvenement>> GetAllByUtilisateurIdAsync(UtilisateurId utilisateurId);
    Task<IEnumerable<PropositionEvenement>> GetAllByDateRangeAsync(DateTime dateDebut, DateTime dateFin);
    Task<IEnumerable<PropositionEvenement>> GetAllByLieuId(Guid LieuId);
    Task<int> GetAllCountAsync();
    Task<int> GetAllCountByStatutAsync(PropositionStatut statut);
    Task<int> GetAllCountByDecisionAsync(PropositionDecision decision);
}
