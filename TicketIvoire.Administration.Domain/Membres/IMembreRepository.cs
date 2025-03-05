namespace TicketIvoire.Administration.Domain.Membres;

public interface IMembreRepository
{
    Task<Membre> GetByIdAsync(MembreId id);
    Task<IEnumerable<Membre>> GetAllAsync(uint? pageNumber, uint? numberByPage);
    Task<IEnumerable<Membre>> GetAllByStatutAdhesionAsync(StatutAdhesion statutAdhesion, uint? pageNumber, uint? numberByPage);
    Task<int> GetCountByStatutAdhesionAsync(StatutAdhesion statutAdhesion);
    Task<int> GetAllCountAsync();
}
