using TicketIvoire.Administration.Domain.LieuEvenements.Rules;

namespace TicketIvoire.Administration.Domain.Tests.LieuEvenements.Rules;

public class LieuInformationMustBeValidRuleTests
{
    [Fact]
    public void GivenMessage_WhenCalled_ThenReturnCorrectText()
    {
        // Arrange 
        (string nom, string description, string adresse, string ville) = ("Nom", "Description", "Adresse", "Ville");
        var rule = new LieuInformationMustBeValidRule(nom, description, adresse, ville);
        var expected = "Le lieu doit avoir un nom, une adresse, une description et une ville bien définie.";

        // Act
        var result = rule.Message;

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("", "Description", "Adresse", "Ville")]
    [InlineData("Nom", "", "Adresse", "Ville")]
    [InlineData("Nom", "Description", "", "Ville")]
    [InlineData("Nom", "Description", "Adresse", "")]
    public void GivenValidate_WhenInvalidInformation_ThenReturnFalse(string nom, string description, string adresse, string ville)
    {
        // Arrange 
        var rule = new LieuInformationMustBeValidRule(nom, description, adresse, ville);

        // Act
        var result = rule.Validate();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void GivenValidate_WhenInvalidInformation_ThenReturnTrue()
    {
        // Arrange 
        (string nom, string description, string adresse, string ville) = ("Nom", "Description", "Adresse", "Ville");
        var rule = new LieuInformationMustBeValidRule(nom, description, adresse, ville);

        // Act
        var result = rule.Validate();

        // Assert
        Assert.True(result);
    }
}
