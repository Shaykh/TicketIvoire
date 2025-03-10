namespace TicketIvoire.Administration.Domain.LieuEvenements;

public interface ILieuRepository
{
    Task<Lieu> GetByIdAsync(Guid lieuId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Lieu>> GetAllAsync(uint? pageNumber, uint? numberByPage, CancellationToken cancellationToken = default);
    Task<IEnumerable<Lieu>> GetAllByVilleAsync(string ville, CancellationToken cancellationToken = default);
    Task<IEnumerable<Lieu>> GetAllByCapaciteRangeAsync(uint? minimum, uint? maximum, CancellationToken cancellationToken = default);
    Task<int> GetCountAsync(CancellationToken cancellationToken = default);
    Task<int> GetCountByVilleAsync(string ville, CancellationToken cancellationToken = default);
}
