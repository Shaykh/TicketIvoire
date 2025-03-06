using TicketIvoire.Administration.Domain.PropositionEvenements;
using TicketIvoire.Administration.Domain.Utilisateurs;

namespace TicketIvoire.Administration.Infrastructure.Persistence.PropositionEvenements;

public static class Mapping
{
    public static PropositionEvenementEntity ToEntity(this PropositionEvenement propositionEvenement) 
        => new()
        { 
            DateDebut = propositionEvenement.DateDebut,
            DateFin = propositionEvenement.DateFin, 
            Decision = propositionEvenement.PropositionDecision, 
            Description = propositionEvenement.Description, 
            Id = propositionEvenement.Id.Value, 
            Lieu = propositionEvenement.Lieu, 
            Nom = propositionEvenement.Nom, 
            PropositionStatut = propositionEvenement.PropositionStatut, 
            UtilisateurId = propositionEvenement.UtilisateurId.Value 
        };

    public static PropositionEvenement ToDomain(this PropositionEvenementEntity propositionEntity)
        => new()
        {
            DateDebut = propositionEntity.DateDebut,
            DateFin = propositionEntity.DateFin,
            Description = propositionEntity.Description,
            Id = new PropositionEvenementId(propositionEntity.Id),
            Lieu = propositionEntity.Lieu,
            Nom = propositionEntity.Nom,
            PropositionDecision = propositionEntity.Decision,
            PropositionStatut = propositionEntity.PropositionStatut,
            UtilisateurId = new UtilisateurId(propositionEntity.UtilisateurId)
        };
}
