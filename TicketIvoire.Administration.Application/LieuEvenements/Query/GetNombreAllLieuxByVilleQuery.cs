using FluentValidation;
using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.LieuEvenements;
using TicketIvoire.Shared.Application.Queries;

namespace TicketIvoire.Administration.Application.LieuEvenements.Query;

public record GetNombreAllLieuxByVilleQuery(string Ville) : IQuery<int>;

public class GetNombreAllLieuxByVilleQueryValidator : AbstractValidator<GetNombreAllLieuxByVilleQuery>
{
    public GetNombreAllLieuxByVilleQueryValidator() => RuleFor(x => x.Ville).NotEmpty();
}

public class GetNombreAllLieuxByVilleQueryHandler(ILogger<GetNombreAllLieuxByVilleQueryHandler> logger,
    IValidator<GetNombreAllLieuxByVilleQuery> validator,
    ILieuRepository lieuRepository) : IQueryHandler<GetNombreAllLieuxByVilleQuery, int>
{
    public async Task<int> Handle(GetNombreAllLieuxByVilleQuery request, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);
        logger.LogInformation("Entree Query Get Nombre All Lieux By Ville: {Ville}", request.Ville);
        int nombre = await lieuRepository.GetCountByVilleAsync(request.Ville);
        logger.LogInformation("Sortie Query Get Nombre All Lieux By Ville: {Ville} : {Count}", request.Ville, nombre);
        return nombre;
    }
}
