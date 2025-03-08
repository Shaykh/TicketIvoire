using TicketIvoire.Administration.Domain.PropositionEvenements.Rules;

namespace TicketIvoire.Administration.Domain.Tests.PropositionEvenements.Rules;

public class PropositionRefusMustHaveRaisonsRuleTests
{
    [Fact]
    public void GivenMessage_WhenCalled_ThenReturnCorrectText()
    {
        // Arrange
        const string raisons = "raisons";
        var rule = new PropositionRefusMustHaveRaisonsRule(raisons);
        var expected = "Les raisons du refus doivent être renseignées.";

        // Act
        var result = rule.Message;

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GivenValidate_WhenValidRaisons_ThenReturnTrue()
    {
        // Arrange
        const string raisons = "raisons";
        var rule = new PropositionRefusMustHaveRaisonsRule(raisons);

        // Act
        var result = rule.Validate();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void GivenValidate_WhenInvalidRaisons_ThenReturnFalse()
    {
        // Arrange
        const string raisons = "";
        var rule = new PropositionRefusMustHaveRaisonsRule(raisons);

        // Act
        var result = rule.Validate();

        // Assert
        Assert.False(result);
    }
}
