namespace TicketIvoire.Administration.Domain.LieuEvenements;

public interface ILieuRepository
{
    Task<Lieu> GetLieuByIdAsync(Guid lieuId);
    Task<IEnumerable<Lieu>> GetAllLieuxAsync(uint? pageNumber, uint? numberByPage);
    Task<IEnumerable<Lieu>> GetLieuxByVilleAsync(string ville);
    Task<IEnumerable<Lieu>> GetLieuxByCapaciteRangeAsync(uint? minimum, uint? maximum);
}
