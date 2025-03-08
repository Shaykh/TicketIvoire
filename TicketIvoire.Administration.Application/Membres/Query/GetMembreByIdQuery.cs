using FluentValidation;
using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.Membres;
using TicketIvoire.Shared.Application.Queries;

namespace TicketIvoire.Administration.Application.Membres.Query;

public record GetMembreByIdQuery(Guid Id) : IQuery<GetMembreResponse>;

public class GetMembreByIdQueryValidator : AbstractValidator<GetMembreByIdQuery>
{
    public GetMembreByIdQueryValidator() => RuleFor(q => q.Id).NotEmpty();
}

public class GetMembreByIdQueryHandler(ILogger<GetMembreByIdQueryHandler> logger,
    IValidator<GetMembreByIdQuery> validator,
    IMembreRepository membreRepository) : IQueryHandler<GetMembreByIdQuery, GetMembreResponse>
{
    public async Task<GetMembreResponse> Handle(GetMembreByIdQuery query, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(query, cancellationToken);
        logger.LogInformation("Entree Query GetMembre By Id {Id}", query.Id);
        Membre membre = await membreRepository.GetByIdAsync(new MembreId(query.Id));

        GetMembreResponse response = membre.ToResponse();
        logger.LogInformation("Sortie Query GetMembre By Id {Response}", response);
        return response;
    }
}
