using Microsoft.EntityFrameworkCore;
using TicketIvoire.Administration.Domain.LieuEvenements;
using TicketIvoire.Shared.Application.Exceptions;
using TicketIvoire.Shared.Infrastructure.Persistence;

namespace TicketIvoire.Administration.Infrastructure.Persistence.LieuEvenements;

public class LieuRepository(AdministrationDbContext dbContext) : ILieuRepository
{
    private readonly DbSet<LieuEntity> _dbSet = dbContext.Lieux;

    public async Task<IEnumerable<Lieu>> GetAllAsync(uint? pageNumber, uint? numberByPage, CancellationToken cancellationToken = default) 
        => await _dbSet
            .AsNoTracking()
            .ToPaging(pageNumber, numberByPage)
            .Select(l => l.ToDomain())
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<Lieu>> GetAllByCapaciteRangeAsync(uint? minimum, uint? maximum, CancellationToken cancellationToken = default)
    {
        int min = minimum is null ? 0 : (int)minimum.Value;
        int max = maximum is null ? int.MaxValue : (int)maximum.Value;
        return await _dbSet
                .AsNoTracking()
                .Where(l => l.Capacite >= min && l.Capacite <= max)
                .Select(l => l.ToDomain())
                .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Lieu>> GetAllByVilleAsync(string ville, CancellationToken cancellationToken = default) 
        => await _dbSet
            .AsNoTracking()
            .Where(l => l.Ville.Contains(ville))
            .Select(l => l.ToDomain())
            .ToListAsync(cancellationToken);

    public async Task<Lieu> GetByIdAsync(Guid lieuId, CancellationToken cancellationToken = default)
    {
        LieuEntity entity = await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.Id == lieuId, cancellationToken) 
            ?? throw new NotFoundException($"Aucun lieu avec l'identifiant {lieuId} n'a été trouvé");

        return entity.ToDomain();
    }

    public async Task<int> GetCountAsync(CancellationToken cancellationToken = default) 
        => await _dbSet
                    .CountAsync(cancellationToken);

    public async Task<int> GetCountByVilleAsync(string ville, CancellationToken cancellationToken = default) 
        => await _dbSet
                    .CountAsync(l => l.Ville.Contains(ville), cancellationToken);
}
