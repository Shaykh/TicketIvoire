using System.Diagnostics.CodeAnalysis;
using TicketIvoire.Administration.Domain.PropositionEvenements.Events;
using TicketIvoire.Administration.Domain.PropositionEvenements.Rules;
using TicketIvoire.Administration.Domain.Utilisateurs;
using TicketIvoire.Shared.Domain;
using TicketIvoire.Shared.Domain.BusinessRules;

namespace TicketIvoire.Administration.Domain.PropositionEvenements;

public class PropositionEvenement : EntityBase, IAggregateRoot
{
    public required PropositionEvenementId Id { get; set; }
    public required string Nom { get; set; }
    public required string Description { get; set; }
    public required DateTime DateDebut { get; set; }
    public required DateTime DateFin { get; set; }
    public required PropositionLieu Lieu { get; set; }
    public PropositionStatut PropositionStatut { get; set; }
    public PropositionDecision? PropositionDecision { get; set; }
    public required UtilisateurId UtilisateurId { get; set; }

    [SetsRequiredMembers]
    private PropositionEvenement(PropositionEvenementId id, UtilisateurId utilisateurId, string nom, string description, DateTime dateDebut, DateTime dateFin, PropositionLieu lieu)
    {
        Id = id;
        Nom = nom;
        Description = description;
        DateDebut = dateDebut;
        DateFin = dateFin;
        Lieu = lieu;
        UtilisateurId = utilisateurId;
        PropositionStatut = PropositionStatut.AVerifier;
        RegisterEvent(new PropositionEvenementCreeEvent(Id.Value, UtilisateurId.Value, Nom, Description, DateDebut, DateFin, Lieu));
    }

    public static PropositionEvenement Create(PropositionEvenementId id, UtilisateurId utilisateurId, string nom, string description, DateTime dateDebut, DateTime dateFin, PropositionLieu lieu)
    {
        CheckRule(new IdMustBeValidRule(id.Value));
        CheckRule(new DatesMustBeValidRule(dateDebut, dateFin));
        CheckRule(new PropositionLieuMustBeValideRule(lieu));
        var newPropositionEvenement = new PropositionEvenement(id, utilisateurId, nom, description, dateDebut, dateFin, lieu);
        return newPropositionEvenement;
    }

    public void Verifier()
    {
        PropositionStatut = PropositionStatut.Verifiee;
        RegisterEvent(new PropositionEvenementVerifieeEvent(Id.Value));
    }

    public void Accepter(UtilisateurId utilisateurId, DateTime dateDecision)
    {
        PropositionDecision = new (utilisateurId, dateDecision, PropositionDecision.AccepterCode, null);
        RegisterEvent(new PropositionEvenementAccepteeEvent(Id.Value, utilisateurId.Value, dateDecision));
    }

    public void Refuser(UtilisateurId utilisateurId, DateTime dateDecision, string raisons)
    {
        CheckRule(new PropositionRefusMustHaveRaisonsRule(raisons));
        PropositionDecision = new(utilisateurId, dateDecision, PropositionDecision.RefuserCode, raisons);
        RegisterEvent(new PropositionEvenementRefuseeEvent(Id.Value, utilisateurId.Value, dateDecision, raisons));
    }

    public bool EstAcceptee() => PropositionDecision?.Code == PropositionDecision.AccepterCode;
    public bool EstRefusee() => PropositionDecision?.Code == PropositionDecision.RefuserCode;
    public bool EstEnAttente() => PropositionDecision is null;
}
