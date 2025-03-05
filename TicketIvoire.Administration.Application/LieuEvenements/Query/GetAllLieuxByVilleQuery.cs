﻿using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.LieuEvenements;
using TicketIvoire.Shared.Application.Queries;

namespace TicketIvoire.Administration.Application.LieuEvenements.Query;

public record GetAllLieuxByVilleQuery(string Ville) : IQuery<IEnumerable<GetLieuResponse>>;

public class GetAllLieuxByVilleQueryHandler(ILogger<GetAllLieuxByVilleQueryHandler> logger,
    ILieuRepository lieuRepository) : IQueryHandler<GetAllLieuxByVilleQuery, IEnumerable<GetLieuResponse>>
{
    public async Task<IEnumerable<GetLieuResponse>> Handle(GetAllLieuxByVilleQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Entree Query Get All Lieux By Ville: {Ville}", query.Ville);
        IEnumerable<Lieu> lieux = await lieuRepository.GetAllByVilleAsync(query.Ville);
        IEnumerable<GetLieuResponse> response = lieux.Select(lieu => lieu.ToGetLieuResponse());
        logger.LogInformation("Sortie Query Get All Lieux By Ville: {Ville}, Nombre: {Count}", query.Ville, response.Count());
        return response;
    }
}
