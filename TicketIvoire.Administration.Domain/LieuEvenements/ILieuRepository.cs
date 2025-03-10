namespace TicketIvoire.Administration.Domain.LieuEvenements;

public interface ILieuRepository
{
    Task<Lieu> GetByIdAsync(Guid lieuId);
    Task<IEnumerable<Lieu>> GetAllAsync(uint? pageNumber, uint? numberByPage);
    Task<IEnumerable<Lieu>> GetAllByVilleAsync(string ville);
    Task<IEnumerable<Lieu>> GetAllByCapaciteRangeAsync(uint? minimum, uint? maximum);
    Task<int> GetCountAsync();
    Task<int> GetCountByVilleAsync(string ville);
}
