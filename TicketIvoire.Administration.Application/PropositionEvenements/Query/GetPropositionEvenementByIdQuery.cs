using FluentValidation;
using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.PropositionEvenements;
using TicketIvoire.Shared.Application.Queries;

namespace TicketIvoire.Administration.Application.PropositionEvenements.Query;

public record GetPropositionEvenementByIdQuery(Guid Id) : IQuery<PropositionEvenementResponse>;

public class GetPropositionEvenementByIdQueryValidator : AbstractValidator<GetPropositionEvenementByIdQuery>
{
    public GetPropositionEvenementByIdQueryValidator() => RuleFor(q => q.Id).NotEmpty();
}

public class GetPropositionEvenementByIdQueryHandler(ILogger<GetPropositionEvenementByIdQueryHandler> logger,
    IValidator<GetPropositionEvenementByIdQuery> validator,
    IPropositionEvenementRepository propositionEvenementRepository) : IQueryHandler<GetPropositionEvenementByIdQuery, PropositionEvenementResponse>
{
    public async Task<PropositionEvenementResponse> Handle(GetPropositionEvenementByIdQuery query, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(query, cancellationToken);
        logger.LogInformation("Entree Query Get Proposition Evenement By Id: {Id}", query.Id);
        PropositionEvenement propositionEvenement = await propositionEvenementRepository.GetByIdAsync(new PropositionEvenementId(query.Id), cancellationToken);

        PropositionEvenementResponse response = propositionEvenement.ToResponse();
        logger.LogInformation("Sortie Query Get Proposition Evenement By Id Response: {Response}", response);
        return response;
    }
}
