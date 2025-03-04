namespace TicketIvoire.Administration.Domain.Membres;

public interface IMembreRepository
{
    Task<Membre> GetByIdAsync(MembreId id);
    Task<Membre> GetByLoginAsync(string login);
    Task<Membre> GetByEmailAsync(string email);
    Task<Membre> GetByTelephoneAsync(string telephone);
    Task<IEnumerable<Membre>> GetAllAsync(string? name);
}
