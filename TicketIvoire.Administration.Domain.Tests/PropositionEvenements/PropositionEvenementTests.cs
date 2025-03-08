using TicketIvoire.Administration.Domain.PropositionEvenements;
using TicketIvoire.Administration.Domain.PropositionEvenements.Events;
using TicketIvoire.Administration.Domain.Utilisateurs;
using TicketIvoire.Shared.Domain.Exceptions;

namespace TicketIvoire.Administration.Domain.Tests.PropositionEvenements;

public class PropositionEvenementTests
{
    [Fact]
    public void GivenCreate_WhenValidData_ThenCreatePropositionEvenement()
    {
        // Arrange
        var utilisateurId = new UtilisateurId(Guid.NewGuid());
        const string nom = "Nom";
        const string description = "Description";
        var dateDebut = DateTime.Now;
        var dateFin = dateDebut.AddDays(1);
        var lieu = new PropositionLieu("Lieu", "Adresse", "Ville", null);

        // Act
        var propositionEvenement = PropositionEvenement.Create(utilisateurId, nom, description, dateDebut, dateFin, lieu);

        // Assert
        Assert.Equal(utilisateurId, propositionEvenement.UtilisateurId);
        Assert.Equal(nom, propositionEvenement.Nom);
        Assert.Equal(description, propositionEvenement.Description);
        Assert.Equal(dateDebut, propositionEvenement.DateDebut);
        Assert.Equal(dateFin, propositionEvenement.DateFin);
        Assert.Equal(lieu, propositionEvenement.Lieu);
        Assert.Equal(PropositionStatut.AVerifier, propositionEvenement.PropositionStatut);
        Assert.True(propositionEvenement.EstEnAttente());
        Assert.False(propositionEvenement.EstAcceptee());
        Assert.False(propositionEvenement.EstRefusee());
        Assert.Contains(propositionEvenement.DomainEvents, e => e.GetType() == typeof(PropositionEvenementCreeEvent));
    }

    [Fact]
    public void GivenCreate_WhenInvalidDates_ThenThrowException()
    {
        // Arrange
        var utilisateurId = new UtilisateurId(Guid.NewGuid());
        const string nom = "Nom";
        const string description = "Description";
        var dateDebut = DateTime.Now;
        var dateFin = dateDebut.AddDays(-1);
        var lieu = new PropositionLieu("Lieu", "Adresse", "Ville", null);

        // Act
        PropositionEvenement act() => PropositionEvenement.Create(utilisateurId, nom, description, dateDebut, dateFin, lieu);

        // Assert
        Assert.Throws<BrokenBusinessRuleException>((Func<PropositionEvenement>)act);
    }

    [Fact]
    public void GivenCreate_WhenInvalidLieu_ThenThrowException()
    {
        // Arrange
        var utilisateurId = new UtilisateurId(Guid.NewGuid());
        const string nom = "Nom";
        const string description = "Description";
        var dateDebut = DateTime.Now;
        var dateFin = dateDebut.AddDays(1);
        PropositionLieu lieu = new(" ", "Adresse", "Ville", null);

        // Act
        PropositionEvenement act() => PropositionEvenement.Create(utilisateurId, nom, description, dateDebut, dateFin, lieu);

        // Assert
        Assert.Throws<BrokenBusinessRuleException>((Func<PropositionEvenement>)act);
    }

    [Fact]
    public void GivenVerifier_WhenCalled_ThenUpdatePropositionStatut()
    {
        // Arrange
        var utilisateurId = new UtilisateurId(Guid.NewGuid());
        var propositionEvenement = PropositionEvenement.Create(utilisateurId, "Nom", "Description", DateTime.Now, DateTime.Now.AddDays(1), new PropositionLieu("Lieu", "Adresse", "Ville", null));

        // Act
        propositionEvenement.Verifier();

        // Assert
        Assert.Equal(PropositionStatut.Verifiee, propositionEvenement.PropositionStatut);
        Assert.Contains(propositionEvenement.DomainEvents, e => e.GetType() == typeof(PropositionEvenementVerifieeEvent));
    }

    [Fact]
    public void GivenAccepter_WhenCalled_ThenUpdatePropositionDecision()
    {
        // Arrange
        var utilisateurId = new UtilisateurId(Guid.NewGuid());
        var propositionEvenement = PropositionEvenement.Create(utilisateurId, "Nom", "Description", DateTime.Now, DateTime.Now.AddDays(1), new PropositionLieu("Lieu", "Adresse", "Ville", null));
        var dateDecision = DateTime.Now;

        // Act
        propositionEvenement.Accepter(utilisateurId, dateDecision);

        // Assert
        Assert.True(propositionEvenement.EstAcceptee());
        Assert.Contains(propositionEvenement.DomainEvents, e => e.GetType() == typeof(PropositionEvenementAccepteeEvent));
    }

    [Fact]
    public void GivenRefuser_WhenCalled_ThenUpdatePropositionDecision()
    {
        // Arrange
        var utilisateurId = new UtilisateurId(Guid.NewGuid());
        var propositionEvenement = PropositionEvenement.Create(utilisateurId, "Nom", "Description", DateTime.Now, DateTime.Now.AddDays(1), new PropositionLieu("Lieu", "Adresse", "Ville", null));
        var dateDecision = DateTime.Now;
        const string raisons = "raisons";

        // Act
        propositionEvenement.Refuser(utilisateurId, dateDecision, raisons);

        // Assert
        Assert.True(propositionEvenement.EstRefusee());
        Assert.Contains(propositionEvenement.DomainEvents, e => e.GetType() == typeof(PropositionEvenementRefuseeEvent));
    }

    [Fact]
    public void GivenRefuser_WhenInvalidRaisons_ThenThrowException()
    {
        // Arrange
        var utilisateurId = new UtilisateurId(Guid.NewGuid());
        var propositionEvenement = PropositionEvenement.Create(utilisateurId, "Nom", "Description", DateTime.Now, DateTime.Now.AddDays(1), new PropositionLieu("Lieu", "Adresse", "Ville", null));
        var dateDecision = DateTime.Now;
        const string raisons = "";

        // Act
        void act() => propositionEvenement.Refuser(utilisateurId, dateDecision, raisons);

        // Assert
        Assert.Throws<BrokenBusinessRuleException>(act);
    }
}
