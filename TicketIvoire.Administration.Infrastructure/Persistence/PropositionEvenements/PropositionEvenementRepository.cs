using Microsoft.EntityFrameworkCore;
using TicketIvoire.Administration.Domain.PropositionEvenements;
using TicketIvoire.Administration.Domain.Utilisateurs;
using TicketIvoire.Shared.Infrastructure.Persistence;

namespace TicketIvoire.Administration.Infrastructure.Persistence.PropositionEvenements;

public class PropositionEvenementRepository(AdministrationDbContext dbContext) : IPropositionEvenementRepository
{
    private readonly DbSet<PropositionEvenementEntity> _dbSet = dbContext.PropositionEvenements;

    public async Task<IEnumerable<PropositionEvenement>> GetAllAsync(uint? pageNumber, uint? numberByPage) 
        => await _dbSet
            .AsNoTracking()
            .ToPaging(pageNumber, numberByPage)
            .Select(e => e.ToDomain())
            .ToListAsync();

    public async Task<IEnumerable<PropositionEvenement>> GetAllByDateRangeAsync(DateTime dateDebut, DateTime dateFin) 
        => await _dbSet
            .AsNoTracking()
            .Where(e => e.DateDebut >= dateDebut && e.DateFin <= dateFin)
            .Select(e => e.ToDomain())
            .ToListAsync();

    public async Task<IEnumerable<PropositionEvenement>> GetAllByDecisionAsync(PropositionDecision decision, uint? pageNumber, uint? numberByPage) 
        => await _dbSet
            .AsNoTracking()
            .Where(e => e.Decision == decision)
            .ToPaging(pageNumber, numberByPage)
            .Select(e => e.ToDomain())
            .ToListAsync();

    public async Task<IEnumerable<PropositionEvenement>> GetAllByLieuId(Guid LieuId) 
        => await _dbSet
            .AsNoTracking()
            .Where(e => e.Lieu.LieuEvenementId == LieuId)
            .Select(e => e.ToDomain())
            .ToListAsync();

    public async Task<IEnumerable<PropositionEvenement>> GetAllByStatutAsync(PropositionStatut statut, uint? pageNumber, uint? numberByPage)
        => await _dbSet
            .AsNoTracking()
            .Where(e => e.PropositionStatut == statut)
            .ToPaging(pageNumber, numberByPage)
            .Select(e => e.ToDomain())
            .ToListAsync();

    public async Task<IEnumerable<PropositionEvenement>> GetAllByUtilisateurIdAsync(UtilisateurId utilisateurId) 
        => await _dbSet
            .AsNoTracking()
            .Where(e => e.UtilisateurId == utilisateurId.Value)
            .Select(e => e.ToDomain())
            .ToListAsync();

    public async Task<int> GetAllCountAsync() 
        => await _dbSet
            .CountAsync();

    public async Task<int> GetAllCountByDecisionAsync(PropositionDecision decision)
        => await _dbSet
            .CountAsync(e => e.Decision == decision);

    public async Task<int> GetAllCountByStatutAsync(PropositionStatut statut) 
        => await _dbSet
            .CountAsync(e => e.PropositionStatut == statut);

    public async Task<PropositionEvenement> GetByIdAsync(PropositionEvenementId id)
    {
        PropositionEvenementEntity entity = await _dbSet
            .AsNoTracking()
            .SingleOrDefaultAsync(e => e.Id == id.Value)
            ?? throw new DataAccessException($"Impossible de trouver la proposition d'évenement avec l'identifiant {id.Value}");
        return entity.ToDomain();
    }
}
