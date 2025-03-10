using Microsoft.EntityFrameworkCore;
using TicketIvoire.Administration.Domain.Membres;
using TicketIvoire.Shared.Application.Exceptions;
using TicketIvoire.Shared.Infrastructure.Persistence;

namespace TicketIvoire.Administration.Infrastructure.Persistence.Membres;

public class MembreRepository(AdministrationDbContext dbContext) : IMembreRepository
{
    private readonly DbSet<MembreEntity> _membres = dbContext.Membres;

    public async Task<IEnumerable<Membre>> GetAllAsync(uint? pageNumber, uint? numberByPage) 
        => await _membres
            .AsNoTracking()
            .ToPaging(pageNumber, numberByPage)
            .Select(m => m.ToDomain())
            .ToListAsync();

    public async Task<IEnumerable<Membre>> GetAllByStatutAdhesionAsync(StatutAdhesion statutAdhesion, uint? pageNumber, uint? numberByPage) 
        => await _membres
            .AsNoTracking()
            .Where(m => m.StatutAdhesion == statutAdhesion)
            .ToPaging(pageNumber, numberByPage)
            .Select(m => m.ToDomain())
            .ToListAsync();

    public async Task<int> GetAllCountAsync() 
        => await _membres
            .CountAsync();

    public async Task<Membre> GetByIdAsync(MembreId id)
    {
        MembreEntity membre = await _membres
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id.Value) ??
            throw new NotFoundException($"Le membre d'identifiant {id.Value} n'a pas été trouvé");
        return membre.ToDomain();
    }

    public async Task<int> GetCountByStatutAdhesionAsync(StatutAdhesion statutAdhesion) 
        => await _membres
            .CountAsync(m => m.StatutAdhesion == statutAdhesion);
}
