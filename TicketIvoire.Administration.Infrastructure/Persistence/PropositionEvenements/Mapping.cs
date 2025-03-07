using TicketIvoire.Administration.Domain.PropositionEvenements;
using TicketIvoire.Administration.Domain.PropositionEvenements.Events;
using TicketIvoire.Administration.Domain.Utilisateurs;

namespace TicketIvoire.Administration.Infrastructure.Persistence.PropositionEvenements;

public static class Mapping
{
    public static PropositionEvenementEntity ToEntity(this PropositionEvenementCreeEvent propositionEvenement) 
        => new()
        { 
            DateDebut = propositionEvenement.DateDebut,
            DateFin = propositionEvenement.DateFin, 
            Description = propositionEvenement.Description, 
            Id = propositionEvenement.Id, 
            Lieu = propositionEvenement.Lieu, 
            Nom = propositionEvenement.Nom, 
            UtilisateurId = propositionEvenement.UtilisateurId 
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
