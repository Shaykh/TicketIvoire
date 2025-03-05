using TicketIvoire.Administration.Domain.PropositionEvenements;

namespace TicketIvoire.Administration.Application.PropositionEvenements;

public static class Mapping
{
    public static PropositionEvenementResponse ToResponse(this PropositionEvenement propositionEvenement)
    {
        bool hasLieuNom = propositionEvenement.Lieu.Nom != null && propositionEvenement.Lieu.Description != null && propositionEvenement.Lieu.Ville != null;
        string? lieuNom = hasLieuNom ? 
            $"{propositionEvenement.Lieu.Nom}, {propositionEvenement.Lieu.Description}, {propositionEvenement.Lieu.Ville}"
            : null;
        return new(propositionEvenement.Id.Value,
                propositionEvenement.Nom,
                propositionEvenement.Description,
                propositionEvenement.DateDebut,
                propositionEvenement.DateFin,
                propositionEvenement.Lieu.LieuEvenementId,
                lieuNom,
                propositionEvenement.PropositionStatut.ToDto(),
                propositionEvenement.PropositionDecision.ToDto(),
                propositionEvenement.UtilisateurId.Value);
    }

    public static PropositionDecisionDto? ToDto(this PropositionDecision? propositionDecision)
        => propositionDecision == null ? 
        null : 
        new(propositionDecision.UtilisateurId, propositionDecision.DateDecision, propositionDecision.Code, propositionDecision.Raisons);

    public static EnumDto ToDto(this PropositionStatut propositionStatut)
        => new((int)propositionStatut, propositionStatut.GetLabel());

    public static string GetLabel(this PropositionStatut propositionStatut)
        => propositionStatut switch
        {
            PropositionStatut.AVerifier => "A Vérifier",
            PropositionStatut.Verifiee => "Vérifiée",
            _ => throw new ArgumentOutOfRangeException(nameof(propositionStatut), propositionStatut, null)
        };
}
