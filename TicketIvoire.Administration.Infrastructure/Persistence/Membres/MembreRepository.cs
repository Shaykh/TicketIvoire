using TicketIvoire.Administration.Domain.Membres;

namespace TicketIvoire.Administration.Infrastructure.Persistence.Membres;

public class MembreRepository : IMembreRepository
{
    public Task<IEnumerable<Membre>> GetAllAsync(uint? pageNumber, uint? numberByPage) => throw new NotImplementedException();
    public Task<IEnumerable<Membre>> GetAllByStatutAdhesionAsync(StatutAdhesion statutAdhesion, uint? pageNumber, uint? numberByPage) => throw new NotImplementedException();
    public Task<int> GetAllCountAsync() => throw new NotImplementedException();
    public Task<Membre> GetByIdAsync(MembreId id) => throw new NotImplementedException();
    public Task<int> GetCountByStatutAdhesionAsync(StatutAdhesion statutAdhesion) => throw new NotImplementedException();
}
