using TicketIvoire.Administration.Domain.LieuEvenements.Rules;

namespace TicketIvoire.Administration.Domain.Tests.LieuEvenements.Rules;

public class CoordonneesGeographiquesMustBeValidRuleTests
{
    [Fact]
    public void GivenMessage_WhenCalled_ThenReturnCorrectText()
    {
        // Arrange
        var latitude = 5.1545m;
        var longitude = 4.69854m;
        var rule = new CoordonneesGeographiquesMustBeValidRule(latitude, longitude);
        var expected = $"Les coordonnées géographiques {latitude}, {longitude} ne sont pas valides";

        // Act
        var result = rule.Message;

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GivenValidate_WhenValidCoordonneesGeographiques_ThenReturnTrue()
    {
        // Arrange
        var rule = new CoordonneesGeographiquesMustBeValidRule(-4.5554m, 5.6694m);

        // Act
        var result = rule.Validate();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void GivenValidate_WhenInValidCoordonneesGeographiques_ThenReturnFalse()
    {
        // Arrange
        var rule = new CoordonneesGeographiquesMustBeValidRule(-454, 385);

        // Act
        var result = rule.Validate();

        // Assert
        Assert.False(result);
    }

}
