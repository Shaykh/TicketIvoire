using TicketIvoire.Administration.Domain.Utilisateurs;

namespace TicketIvoire.Administration.Domain.PropositionEvenements;

public interface IPropositionEvenementRepository
{
    Task<PropositionEvenement> GetByIdAsync(PropositionEvenementId id, CancellationToken cancellationToken = default);
    Task<IEnumerable<PropositionEvenement>> GetAllAsync(uint? pageNumber, uint? numberByPage, CancellationToken cancellationToken = default);
    Task<IEnumerable<PropositionEvenement>> GetAllByStatutAsync(PropositionStatut statut, uint? pageNumber, uint? numberByPage, CancellationToken cancellationToken = default);
    Task<IEnumerable<PropositionEvenement>> GetAllByDecisionCodeAsync(string decisionCode, uint? pageNumber, uint? numberByPage, CancellationToken cancellationToken = default);
    Task<IEnumerable<PropositionEvenement>> GetAllEnAttenteDeDecisionCodeAsync(uint? pageNumber, uint? numberByPage, CancellationToken cancellationToken = default);
    Task<IEnumerable<PropositionEvenement>> GetAllByUtilisateurIdAsync(UtilisateurId utilisateurId, CancellationToken cancellationToken = default);
    Task<IEnumerable<PropositionEvenement>> GetAllByDateRangeAsync(DateTime dateDebut, DateTime dateFin, CancellationToken cancellationToken = default);
    Task<IEnumerable<PropositionEvenement>> GetAllByLieuIdAsync(Guid LieuId, CancellationToken cancellationToken = default);
    Task<int> GetAllCountAsync(CancellationToken cancellationToken = default);
    Task<int> GetAllCountByStatutAsync(PropositionStatut statut, CancellationToken cancellationToken = default);
    Task<int> GetAllCountByDecisionCodeAsync(string decisionCode, CancellationToken cancellationToken = default);
    Task<int> GetAllCountEnAttenteDecisionAsync(CancellationToken cancellationToken = default);
}
