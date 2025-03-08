using TicketIvoire.Administration.Domain.LieuEvenements.Rules;

namespace TicketIvoire.Administration.Domain.Tests.LieuEvenements.Rules;

public class SuppressionRaisonsMustBeValidRuleTests
{
    [Fact]
    public void GivenMessage_WhenCalled_ThenReturnCorrectText()
    {
        // Arrange 
        string raisons = "raisons";
        var rule = new SuppressionRaisonsMustBeValidRule(raisons);
        var expected = $"Les raisons de suppression {raisons} ne sont pas valides";

        // Act
        var result = rule.Message;

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GivenValidate_WhenValidRaisons_ThenReturnTrue()
    {
        // Arrange
        string raisons = "raisons";
        var rule = new SuppressionRaisonsMustBeValidRule(raisons);

        // Act
        var result = rule.Validate();

        // Assert
        Assert.True(result);
    }

    [Theory]
    [InlineData("                        ")]
    [InlineData("")]
    [InlineData(" ")]
    public void GivenValidate_WhenInValidRaisons_ThenReturnFalse(string raisons)
    {
        // Arrange
        var rule = new SuppressionRaisonsMustBeValidRule(raisons);

        // Act
        var result = rule.Validate();

        // Assert
        Assert.False(result);
    }
}
