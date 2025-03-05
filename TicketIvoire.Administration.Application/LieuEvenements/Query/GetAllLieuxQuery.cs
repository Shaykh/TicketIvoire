using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.LieuEvenements;
using TicketIvoire.Shared.Application.Queries;

namespace TicketIvoire.Administration.Application.LieuEvenements.Query;

public record GetAllLieuxQuery(uint? PageNumber, uint? NumberByPage) : IQuery<IEnumerable<GetLieuResponse>>;

public class GetAllLieuxQueryHandler(ILogger<GetAllLieuxQueryHandler> logger,
    ILieuRepository lieuRepository) : IQueryHandler<GetAllLieuxQuery, IEnumerable<GetLieuResponse>>
{
    public async Task<IEnumerable<GetLieuResponse>> Handle(GetAllLieuxQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Entree Query Get All Lieux");
        IEnumerable<Lieu> lieux = await lieuRepository.GetAllLieuxAsync(query.PageNumber, query.NumberByPage);
        IEnumerable<GetLieuResponse> response = lieux.Select(lieu => lieu.ToGetLieuResponse());
        logger.LogInformation("Sortie Query Get All Lieux Nombre: {Count}", response.Count());
        return response;
    }
}   
