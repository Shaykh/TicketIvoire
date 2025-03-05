using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.LieuEvenements;
using TicketIvoire.Shared.Application.Queries;

namespace TicketIvoire.Administration.Application.LieuEvenements.Query;

public record GetLieuxByCapaciteRangeQuery(uint Minimum, uint Maximum) : IQuery<IEnumerable<GetLieuResponse>>;

public class GetLieuxByCapaciteRangeQueryHandler(ILogger<GetLieuxByCapaciteRangeQueryHandler> logger,
    ILieuRepository lieuRepository) : IQueryHandler<GetLieuxByCapaciteRangeQuery, IEnumerable<GetLieuResponse>>
{
    public async Task<IEnumerable<GetLieuResponse>> Handle(GetLieuxByCapaciteRangeQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Entree Query Get Lieux By Capacite Range: {Minimum} - {Maximum}", query.Minimum, query.Maximum);
        IEnumerable<Lieu> lieux = await lieuRepository.GetAllByCapaciteRangeAsync(query.Minimum, query.Maximum);
        IEnumerable<GetLieuResponse> response = lieux.Select(lieu => lieu.ToGetLieuResponse());
        logger.LogInformation("Sortie Query Get Lieux By Capacite from: {Minimum} to {Maximum}, Nombre: {Count}", query.Minimum, query.Maximum, response.Count());
        return response;
    }
}
