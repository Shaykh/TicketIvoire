using TicketIvoire.Administration.Domain.PropositionEvenements;
using TicketIvoire.Administration.Domain.PropositionEvenements.Rules;

namespace TicketIvoire.Administration.Domain.Tests.PropositionEvenements.Rules;

public class PropositionLieuMustBeValidRuleTests
{
    [Fact]
    public void GivenMessage_WhenCalled_ThenReturnCorrectText()
    {
        // Arrange
        var lieu = new PropositionLieu("Lieu", "Adresse", "Ville", null);
        var rule = new PropositionLieuMustBeValidRule(lieu);
        var expected = "Le lieu de la proposition doit être correctement renseigné.";

        // Act
        var result = rule.Message;

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GivenValidate_WhenValidLieuInformation_ThenReturnTrue()
    {
        // Arrange
        var lieu = new PropositionLieu("Lieu", "Adresse", "Ville", null);
        var rule = new PropositionLieuMustBeValidRule(lieu);

        // Act
        var result = rule.Validate();

        // Assert
        Assert.True(result);
    }

    [Theory]
    [InlineData("", "Adresse", "Ville")]
    [InlineData("Lieu", "", "Ville")]
    [InlineData("Lieu", "Adresse", "")]
    [InlineData(" ", " ", " ")]
    public void GivenValidate_WhenValidLieuId_ThenReturnTrue(string nom, string description, string ville)
    {
        // Arrange
        var lieu = new PropositionLieu(nom, description, ville, Guid.NewGuid());
        var rule = new PropositionLieuMustBeValidRule(lieu);

        // Act
        var result = rule.Validate();

        // Assert
        Assert.True(result);
    }

    [Theory]
    [InlineData("", "Adresse", "Ville")]
    [InlineData("Lieu", "", "Ville")]
    [InlineData("Lieu", "Adresse", "")]
    public void GivenValidate_WhenInvalidLieuInformationAndLieuId_ThenReturnFalse(string nom, string description, string ville)
    {
        // Arrange
        var lieu = new PropositionLieu(nom, description, ville, null);
        var rule = new PropositionLieuMustBeValidRule(lieu);

        // Act
        var result = rule.Validate();

        // Assert
        Assert.False(result);
    }
}
