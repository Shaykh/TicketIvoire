using TicketIvoire.Administration.Domain.LieuEvenements;
using TicketIvoire.Administration.Domain.LieuEvenements.Events;
using TicketIvoire.Administration.Domain.Utilisateurs;
using TicketIvoire.Shared.Domain.Exceptions;

namespace TicketIvoire.Administration.Domain.Tests.LieuEvenements;

public class LieuTests
{
    [Fact]
    public void GivenCreate_WhenValidData_ThenCreateLieu()
    {
        // Arrange
        const string nom = "Nom";
        const string description = "Description";
        const string adresse = "Adresse";
        const string ville = "Ville";
        uint? capacite = null;

        // Act
        var lieu = Lieu.Create(nom, description, adresse, ville, capacite);

        // Assert
        Assert.Equal(capacite, lieu.Capacite);
        Assert.Equal(nom, lieu.Nom);
        Assert.Equal(description, lieu.Description);
        Assert.Equal(adresse, lieu.Adresse);
        Assert.Equal(ville, lieu.Ville);
        Assert.Contains(lieu.DomainEvents, e => e.GetType() == typeof(LieuAjouteEvent));
    }

    [Fact]
    public void GivenCreate_WhenInValidNom_ThenThrowException()
    {
        // Arrange
        const string nom = "";
        const string description = "Description";
        const string adresse = "Adresse";
        const string ville = "Ville";
        const uint capacite = 455;

        // Act
        static Lieu act() => Lieu.Create(nom, description, adresse, ville, capacite);

        // Assert
        Assert.Throws<BrokenBusinessRuleException>((Func<Lieu>)act);
    }

    [Fact]
    public void GivenCreate_WhenInValidDescription_ThenThrowException()
    {
        // Arrange
        const string nom = "Nom";
        const string description = "";
        const string adresse = "Adresse";
        const string ville = "Ville";
        const uint capacite = 455;

        // Act
        static Lieu act() => Lieu.Create(nom, description, adresse, ville, capacite);

        // Assert
        Assert.Throws<BrokenBusinessRuleException>((Func<Lieu>)act);
    }

    [Fact]
    public void GivenCreate_WhenInValidAdresse_ThenThrowException()
    {
        // Arrange
        const string nom = "Nom";
        const string description = "Description";
        const string adresse = "";
        const string ville = "Ville";
        const uint capacite = 455;

        // Act
        static Lieu act() => Lieu.Create(nom, description, adresse, ville, capacite);

        // Assert
        Assert.Throws<BrokenBusinessRuleException>((Func<Lieu>)act);
    }

    [Fact]
    public void GivenCreate_WhenInValidVille_ThenThrowException()
    {
        // Arrange
        const string nom = "Nom";
        const string description = "Description";
        const string adresse = "Adresse";
        const string ville = "";
        const uint capacite = 455;

        // Act
        static Lieu act() => Lieu.Create(nom, description, adresse, ville, capacite);

        // Assert
        Assert.Throws<BrokenBusinessRuleException>((Func<Lieu>)act);
    }

    [Fact]
    public void GivenModifier_WhenValidData_ThenUpdateLieu()
    {
        // Arrange
        var lieu = Lieu.Create("nom", "description", "adresse", "ville", null);
        const string nom = "Nom";
        const string description = "Description";
        const string adresse = "Adresse";
        const string ville = "Ville";
        uint? capacite = 500;

        // Act
        lieu.Modifier(nom, description, adresse, ville, capacite);

        // Assert
        Assert.Equal(capacite, lieu.Capacite);
        Assert.Equal(nom, lieu.Nom);
        Assert.Equal(description, lieu.Description);
        Assert.Equal(adresse, lieu.Adresse);
        Assert.Equal(ville, lieu.Ville);
        Assert.Contains(lieu.DomainEvents, e => e.GetType() == typeof(LieuModifieEvent));
    }

    [Fact]
    public void GivenModifier_WhenInValidNom_ThenThrowException()
    {
        // Arrange
        var lieu = Lieu.Create("nom", "description", "adresse", "ville", null);
        const string nom = "";
        const string description = "Description";
        const string adresse = "Adresse";
        const string ville = "Ville";
        const uint capacite = 455;

        // Act
        void act() => lieu.Modifier(nom, description, adresse, ville, capacite);

        // Assert
        Assert.Throws<BrokenBusinessRuleException>(act);
    }

    [Fact]
    public void GivenModifier_WhenInValidDescription_ThenThrowException()
    {
        // Arrange
        var lieu = Lieu.Create("nom", "description", "adresse", "ville", null);
        const string nom = "Nom";
        const string description = "";
        const string adresse = "Adresse";
        const string ville = "Ville";
        const uint capacite = 455;

        // Act
        void act() => lieu.Modifier(nom, description, adresse, ville, capacite);

        // Assert
        Assert.Throws<BrokenBusinessRuleException>(act);
    }

    [Fact]
    public void GivenModifier_WhenInValidAdresse_ThenThrowException()
    {
        // Arrange
        var lieu = Lieu.Create("nom", "description", "adresse", "ville", null);
        const string nom = "Nom";
        const string description = "Description";
        const string adresse = "";
        const string ville = "Ville";
        const uint capacite = 455;

        // Act
        void act() => lieu.Modifier(nom, description, adresse, ville, capacite);

        // Assert
        Assert.Throws<BrokenBusinessRuleException>(act);
    }

    [Fact]
    public void GivenModifier_WhenInValidVille_ThenThrowException()
    {
        // Arrange
        var lieu = Lieu.Create("nom", "description", "adresse", "ville", null);
        const string nom = "Nom";
        const string description = "Description";
        const string adresse = "Adresse";
        const string ville = "";
        const uint capacite = 455;

        // Act
        void act() => lieu.Modifier(nom, description, adresse, ville, capacite);

        // Assert
        Assert.Throws<BrokenBusinessRuleException>(act);
    }

    [Fact]
    public void GivenDefinirCoordonneesGeographiques_WhenValidCoordonneesGeographiques_ThenDefineCoordonneesGeographiques()
    {
        // Arrange
        var lieu = Lieu.Create("nom", "description", "adresse", "ville", null);
        var coordonneesGeographiques = new LieuCoordonneesGeographiques(-4.5554m, 5.6694m);

        // Act
        lieu.DefinirCoordonneesGeographiques(coordonneesGeographiques);

        // Assert
        Assert.Equal(coordonneesGeographiques, lieu.CoordonneesGeographiques);
        Assert.Contains(lieu.DomainEvents, e => e.GetType() == typeof(LieuCoordonneesGeographiquesDefiniesEvent));
    }

    [Fact]
    public void GivenDefinirCoordonneesGeographiques_WhenInValidCoordonneesGeographiques_ThenThrowException()
    {
        // Arrange
        var lieu = Lieu.Create("nom", "description", "adresse", "ville", null);
        var coordonneesGeographiques = new LieuCoordonneesGeographiques(-454, 385);

        // Act
        void act() => lieu.DefinirCoordonneesGeographiques(coordonneesGeographiques);

        // Assert
        Assert.Throws<BrokenBusinessRuleException>(act);
    }

    [Fact]
    public void GivenSupprimer_WhenValidRaison_ThenSuppress()
    {
        // Arrange
        var lieu = Lieu.Create("nom", "description", "adresse", "ville", null);
        var raisons = "raisons";
        var utilisateurId = new UtilisateurId(Guid.NewGuid());

        // Act
        lieu.Supprimer(raisons, utilisateurId);

        // Assert
        Assert.Contains(lieu.DomainEvents, e => e.GetType() == typeof(LieuRetireEvent));
    }

    [Fact]
    public void GivenSupprimer_WhenInValidRaisonThenThrowException()
    {
        // Arrange
        var lieu = Lieu.Create("nom", "description", "adresse", "ville", null);
        var raisons = "";
        var utilisateurId = new UtilisateurId(Guid.NewGuid());

        // Act
        void act() => lieu.Supprimer(raisons, utilisateurId);

        // Assert
        Assert.Throws<BrokenBusinessRuleException>(act);
    }
}
