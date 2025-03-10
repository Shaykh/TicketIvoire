using FluentValidation;
using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.PropositionEvenements;
using TicketIvoire.Administration.Domain.Utilisateurs;
using TicketIvoire.Shared.Application.Queries;

namespace TicketIvoire.Administration.Application.PropositionEvenements.Query;

public record GetAllPropositionsByUtilisateurIdQuery(Guid UtilisateurId) : IQuery<IEnumerable<PropositionEvenementResponse>>;

public class GetAllPropositionsByUtilisateurIdQueryValidator : AbstractValidator<GetAllPropositionsByUtilisateurIdQuery>
{
    public GetAllPropositionsByUtilisateurIdQueryValidator() => RuleFor(q => q.UtilisateurId).NotEmpty();
}

public class GetAllPropositionsByUtilisateurIdQueryHandler(ILogger<GetAllPropositionsByUtilisateurIdQueryHandler> logger,
    IValidator<GetAllPropositionsByUtilisateurIdQuery> validator,
    IPropositionEvenementRepository propositionEvenementRepository) : IQueryHandler<GetAllPropositionsByUtilisateurIdQuery, IEnumerable<PropositionEvenementResponse>>
{
    public async Task<IEnumerable<PropositionEvenementResponse>> Handle(GetAllPropositionsByUtilisateurIdQuery query, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(query, cancellationToken);
        logger.LogInformation("Entree Query Get All Proposition Evenement By UtilisateurId: {UtilisateurId}", query.UtilisateurId);
        IEnumerable<PropositionEvenement> propositionEvenements = await propositionEvenementRepository.GetAllByUtilisateurIdAsync(new UtilisateurId(query.UtilisateurId), cancellationToken);
        IEnumerable<PropositionEvenementResponse> response = propositionEvenements.Select(pe => pe.ToResponse());
        logger.LogInformation("Sortie Query Get All Proposition Evenement By UtilisateurId: {UtilisateurId}, Response: {Count}", query.UtilisateurId, response.Count());
        return response;
    }
}
