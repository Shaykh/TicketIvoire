using Microsoft.EntityFrameworkCore;
using TicketIvoire.Administration.Domain.LieuEvenements;
using TicketIvoire.Shared.Infrastructure.Persistence;

namespace TicketIvoire.Administration.Infrastructure.Persistence.LieuEvenements;

public class LieuRepository(AdministrationDbContext dbContext) : ILieuRepository
{
    private readonly DbSet<LieuEntity> _dbSet = dbContext.Lieux;

    public async Task<IEnumerable<Lieu>> GetAllAsync(uint? pageNumber, uint? numberByPage) 
        => await _dbSet
            .AsNoTracking()
            .ToPaging(pageNumber, numberByPage)
            .Select(l => l.ToDomain())
            .ToListAsync();

    public async Task<IEnumerable<Lieu>> GetAllByCapaciteRangeAsync(uint? minimum, uint? maximum)
    {
        int min = minimum is null ? 0 : (int)minimum.Value;
        int max = maximum is null ? int.MaxValue : (int)maximum.Value;
        return await _dbSet
                .AsNoTracking()
                .Where(l => l.Capacite >= min && l.Capacite <= max)
                .Select(l => l.ToDomain())
                .ToListAsync();
    }

    public async Task<IEnumerable<Lieu>> GetAllByVilleAsync(string ville) 
        => await _dbSet
            .AsNoTracking()
            .Where(l => l.Ville.Contains(ville))
            .Select(l => l.ToDomain())
            .ToListAsync();

    public async Task<Lieu> GetByIdAsync(Guid lieuId)
    {
        LieuEntity entity = await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.Id == lieuId) 
            ?? throw new DataAccessException($"Aucun lieu avec l'identifiant {lieuId} n'a été trouvé");

        return entity.ToDomain();
    }
}
