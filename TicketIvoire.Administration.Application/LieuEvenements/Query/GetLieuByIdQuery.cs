using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.LieuEvenements;
using TicketIvoire.Shared.Application.Queries;

namespace TicketIvoire.Administration.Application.LieuEvenements.Query;

public record GetLieuByIdQuery(Guid Id) : IQuery<GetLieuResponse>;

public class GetLieuByIdQueryHandler(ILogger<GetLieuByIdQueryHandler> logger,
    ILieuRepository lieuRepository) : IQueryHandler<GetLieuByIdQuery, GetLieuResponse>
{
    public async Task<GetLieuResponse> Handle(GetLieuByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Entree Query Get Lieu By Id: {LieuId}", query.Id);
        Lieu lieu = await lieuRepository.GetByIdAsync(query.Id);
        var response = lieu.ToGetLieuResponse();
        logger.LogInformation("Sortie Query Get Lieu By Id Response: {Response}", response);
        return response;
    }
}
