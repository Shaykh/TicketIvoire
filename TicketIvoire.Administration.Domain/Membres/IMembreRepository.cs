namespace TicketIvoire.Administration.Domain.Membres;

public interface IMembreRepository
{
    Task<Membre> GetByIdAsync(MembreId id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Membre>> GetAllAsync(uint? pageNumber, uint? numberByPage, CancellationToken cancellationToken = default);
    Task<IEnumerable<Membre>> GetAllByStatutAdhesionAsync(StatutAdhesion statutAdhesion, uint? pageNumber, uint? numberByPage, CancellationToken cancellationToken = default);
    Task<int> GetCountByStatutAdhesionAsync(StatutAdhesion statutAdhesion, CancellationToken cancellationToken = default);
    Task<int> GetAllCountAsync(CancellationToken cancellationToken = default);
}
