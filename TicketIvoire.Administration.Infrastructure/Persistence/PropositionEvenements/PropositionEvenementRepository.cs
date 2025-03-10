using Microsoft.EntityFrameworkCore;
using TicketIvoire.Administration.Domain.PropositionEvenements;
using TicketIvoire.Administration.Domain.Utilisateurs;
using TicketIvoire.Shared.Application.Exceptions;
using TicketIvoire.Shared.Infrastructure.Persistence;

namespace TicketIvoire.Administration.Infrastructure.Persistence.PropositionEvenements;

public class PropositionEvenementRepository(AdministrationDbContext dbContext) : IPropositionEvenementRepository
{
    private readonly DbSet<PropositionEvenementEntity> _dbSet = dbContext.PropositionEvenements;

    public async Task<IEnumerable<PropositionEvenement>> GetAllAsync(uint? pageNumber, uint? numberByPage, CancellationToken cancellationToken = default) 
        => await _dbSet
            .AsNoTracking()
            .ToPaging(pageNumber, numberByPage)
            .Select(e => e.ToDomain())
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<PropositionEvenement>> GetAllByDateRangeAsync(DateTime dateDebut, DateTime dateFin, CancellationToken cancellationToken = default) 
        => await _dbSet
            .AsNoTracking()
            .Where(e => e.DateDebut >= dateDebut && e.DateFin <= dateFin)
            .Select(e => e.ToDomain())
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<PropositionEvenement>> GetAllByDecisionCodeAsync(string decisionCode, uint? pageNumber, uint? numberByPage, CancellationToken cancellationToken = default) 
        => await _dbSet
            .AsNoTracking()
            .Where(e => e.Decision != null && e.Decision.Code == decisionCode)
            .ToPaging(pageNumber, numberByPage)
            .Select(e => e.ToDomain())
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<PropositionEvenement>> GetAllEnAttenteDeDecisionCodeAsync(uint? pageNumber, uint? numberByPage, CancellationToken cancellationToken = default) 
        => await _dbSet
                .AsNoTracking()
                .Where(e => e.Decision == null)
                .ToPaging(pageNumber, numberByPage)
                .Select(e => e.ToDomain())
                .ToListAsync(cancellationToken);

    public async Task<IEnumerable<PropositionEvenement>> GetAllByLieuIdAsync(Guid LieuId, CancellationToken cancellationToken = default) 
        => await _dbSet
            .AsNoTracking()
            .Where(e => e.Lieu.LieuEvenementId == LieuId)
            .Select(e => e.ToDomain())
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<PropositionEvenement>> GetAllByStatutAsync(PropositionStatut statut, uint? pageNumber, uint? numberByPage, CancellationToken cancellationToken = default)
        => await _dbSet
            .AsNoTracking()
            .Where(e => e.PropositionStatut == statut)
            .ToPaging(pageNumber, numberByPage)
            .Select(e => e.ToDomain())
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<PropositionEvenement>> GetAllByUtilisateurIdAsync(UtilisateurId utilisateurId, CancellationToken cancellationToken = default) 
        => await _dbSet
            .AsNoTracking()
            .Where(e => e.UtilisateurId == utilisateurId.Value)
            .Select(e => e.ToDomain())
            .ToListAsync(cancellationToken);

    public async Task<int> GetAllCountAsync(CancellationToken cancellationToken = default) 
        => await _dbSet
            .CountAsync(cancellationToken);

    public async Task<int> GetAllCountByDecisionCodeAsync(string decisionCode, CancellationToken cancellationToken = default)
        => await _dbSet
            .CountAsync(e => e.Decision != null && e.Decision.Code == decisionCode, cancellationToken);

    public async Task<int> GetAllCountByStatutAsync(PropositionStatut statut, CancellationToken cancellationToken = default) 
        => await _dbSet
            .CountAsync(e => e.PropositionStatut == statut, cancellationToken);

    public async Task<int> GetAllCountEnAttenteDecisionAsync(CancellationToken cancellationToken = default) 
        => await _dbSet
        .CountAsync(e => e.Decision == null, cancellationToken);

    public async Task<PropositionEvenement> GetByIdAsync(PropositionEvenementId id, CancellationToken cancellationToken = default)
    {
        PropositionEvenementEntity entity = await _dbSet
            .AsNoTracking()
            .SingleOrDefaultAsync(e => e.Id == id.Value, cancellationToken)
            ?? throw new NotFoundException($"Impossible de trouver la proposition d'évenement avec l'identifiant {id.Value}");
        return entity.ToDomain();
    }
}
