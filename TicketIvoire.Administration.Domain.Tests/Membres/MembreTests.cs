using TicketIvoire.Administration.Domain.Membres;
using TicketIvoire.Administration.Domain.Membres.Events;
using TicketIvoire.Shared.Domain.Exceptions;

namespace TicketIvoire.Administration.Domain.Tests.Membres;

public class MembreTests
{
    [Fact]
    public void GivenCreate_WhenValidData_ThenCreateMembre()
    {
        // Arrange
        const string login = "validLogin";
        const string email = "test@example.com";
        const string nom = "Nom";
        const string prenom = "Prenom";
        const string telephone = "0123456789";
        var dateAdhesion = DateTime.Now;

        // Act
        var membre = Membre.Create(login, email, nom, prenom, telephone, dateAdhesion);

        // Assert
        Assert.Equal(login, membre.Login);
        Assert.Equal(email, membre.Email);
        Assert.Equal(nom, membre.Nom);
        Assert.Equal(prenom, membre.Prenom);
        Assert.Equal(telephone, membre.Telephone);
        Assert.Equal(dateAdhesion, membre.DateAdhesion);
        Assert.True(membre.EstActif);
        Assert.Contains(membre.DomainEvents, e => e.GetType() == typeof(MembreCreeEvent));
    }

    [Fact]
    public void GivenCreate_WhenInvalidLogin_ThenThrowException()
    {
        // Arrange
        const string login = "";
        const string email = "test@example.com";
        const string nom = "Nom";
        const string prenom = "Prenom";
        const string telephone = "0123456789";
        var dateAdhesion = DateTime.Now;

        // Act
        Membre act() => Membre.Create(login, email, nom, prenom, telephone, dateAdhesion);

        // Assert
        Assert.Throws<BrokenBusinessRuleException>((Func<Membre>)act);
    }

    [Fact]
    public void GivenCreate_WhenInvalidEmail_ThenThrowException()
    {
        // Arrange
        const string login = "validLogin";
        const string email = "invalid-email";
        const string nom = "Nom";
        const string prenom = "Prenom";
        const string telephone = "0123456789";
        var dateAdhesion = DateTime.Now;

        // Act
        Membre act() => Membre.Create(login, email, nom, prenom, telephone, dateAdhesion);

        // Assert
        Assert.Throws<BrokenBusinessRuleException>((Func<Membre>)act);
    }

    [Fact]
    public void GivenCreate_WhenInvalidTelephone_ThenThrowException()
    {
        // Arrange
        const string login = "validLogin";
        const string email = "test@example.com";
        const string nom = "Nom";
        const string prenom = "Prenom";
        const string telephone = "";
        var dateAdhesion = DateTime.Now;

        // Act
        Membre act() => Membre.Create(login, email, nom, prenom, telephone, dateAdhesion);

        // Assert
        Assert.Throws<BrokenBusinessRuleException>((Func<Membre>)act);
    }

    [Fact]
    public void GivenValider_WhenCalled_ThenUpdateStatutAdhesion()
    {
        // Arrange
        var membre = Membre.Create("login", "test@example.com", "Nom", "Prenom", "0123456789", DateTime.Now);

        // Act
        membre.Valider();

        // Assert
        Assert.Equal(StatutAdhesion.Accepte, membre.StatutAdhesion);
        Assert.Contains(membre.DomainEvents, e => e.GetType() == typeof(MembreValideEvent));
    }

    [Fact]
    public void GivenRefuser_WhenCalled_ThenUpdateStatutAdhesion()
    {
        // Arrange
        var membre = Membre.Create("login", "test@example.com", "Nom", "Prenom", "0123456789", DateTime.Now);

        // Act
        membre.Refuser();

        // Assert
        Assert.Equal(StatutAdhesion.Refuse, membre.StatutAdhesion);
        Assert.Contains(membre.DomainEvents, e => e.GetType() == typeof(MembreRefuseEvent));
    }

    [Fact]
    public void GivenDesactiver_WhenCalled_ThenUpdateEstActif()
    {
        // Arrange
        var membre = Membre.Create("login", "test@example.com", "Nom", "Prenom", "0123456789", DateTime.Now);
        const string raisons = "raisons";

        // Act
        membre.Desactiver(raisons);

        // Assert
        Assert.False(membre.EstActif);
        Assert.Contains(membre.DomainEvents, e => e.GetType() == typeof(MembreDesactiveEvent));
    }

    [Fact]
    public void GivenReactiver_WhenCalled_ThenUpdateEstActif()
    {
        // Arrange
        var membre = Membre.Create("login", "test@example.com", "Nom", "Prenom", "0123456789", DateTime.Now);
        const string raisons = "raisons";
        membre.Desactiver(raisons);

        // Act
        membre.Reactiver(raisons);

        // Assert
        Assert.True(membre.EstActif);
        Assert.Contains(membre.DomainEvents, e => e.GetType() == typeof(MembreReactiveEvent));
    }
}
