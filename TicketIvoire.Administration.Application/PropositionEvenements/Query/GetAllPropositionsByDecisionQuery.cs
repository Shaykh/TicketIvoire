using FluentValidation;
using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.PropositionEvenements;
using TicketIvoire.Shared.Application.Queries;

namespace TicketIvoire.Administration.Application.PropositionEvenements.Query;

public record GetAllPropositionsByDecisionQuery(string DecisionCode, uint? PageNumber, uint? NumberByPage) : IQuery<IEnumerable<PropositionEvenementResponse>>;

public class GetAllPropositionsByDecisionQueryValidator : AbstractValidator<GetAllPropositionsByDecisionQuery>
{
    public GetAllPropositionsByDecisionQueryValidator()
    {
        RuleFor(q => q.DecisionCode)
            .NotEmpty();
        RuleFor(q => q.DecisionCode)
            .Must(dc => dc.Equals(PropositionDecision.AccepterCode, StringComparison.OrdinalIgnoreCase) 
            || dc.Equals(PropositionDecision.RefuserCode, StringComparison.OrdinalIgnoreCase));
    }
}

public class GetAllPropositionsByDecisionQueryHandler(ILogger<GetAllPropositionsByDecisionQueryHandler> logger,
    IValidator<GetAllPropositionsByDecisionQuery> validator,
    IPropositionEvenementRepository propositionEvenementRepository) : IQueryHandler<GetAllPropositionsByDecisionQuery, IEnumerable<PropositionEvenementResponse>>
{
    public async Task<IEnumerable<PropositionEvenementResponse>> Handle(GetAllPropositionsByDecisionQuery request, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);
        logger.LogInformation("Entree Query Get All Proposition Evenement By Decision: {Decision} PageNumber: {PageNumber} NumberByPage: {NumberByPage}", request.DecisionCode, request.PageNumber, request.NumberByPage);
        IEnumerable<PropositionEvenement> propositionEvenements = await propositionEvenementRepository.GetAllByDecisionCodeAsync(request.DecisionCode, request.PageNumber, request.NumberByPage);
        IEnumerable<PropositionEvenementResponse> response = propositionEvenements.Select(pe => pe.ToResponse());
        logger.LogInformation("Sortie Query Get All Proposition Evenement By Decision: {Decision}, Response: {Count}", request.DecisionCode, response.Count());
        return response;
    }
}
