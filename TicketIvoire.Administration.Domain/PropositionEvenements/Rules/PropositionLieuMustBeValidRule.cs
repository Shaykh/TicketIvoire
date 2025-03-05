using TicketIvoire.Shared.Domain.BusinessRules;

namespace TicketIvoire.Administration.Domain.PropositionEvenements.Rules;

public class PropositionLieuMustBeValidRule(PropositionLieu propositionLieu) : IBusinessRule
{
    public string Message => "Le lieu de la proposition doit être correctement renseigné.";
    public bool Validate()
    {
        bool existeDeja = propositionLieu.LieuEvenementId.HasValue && propositionLieu.LieuEvenementId.Value != Guid.Empty;
        bool estNouveauValide = !string.IsNullOrWhiteSpace(propositionLieu.Nom) && !string.IsNullOrWhiteSpace(propositionLieu.Description) && !string.IsNullOrWhiteSpace(propositionLieu.Ville);

        return existeDeja || estNouveauValide;
    }
}
