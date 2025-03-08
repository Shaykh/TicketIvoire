using TicketIvoire.Shared.Domain.BusinessRules;

namespace TicketIvoire.Shared.Domain.Tests.BusinessRules;

public class IdMustBeValidRuleTests
{
    [Fact]
    public void GivenMessage_WhenCalled_ThenReturnCorrectText()
    {
        // Arrange
        var value = Guid.NewGuid();
        var rule = new IdMustBeValidRule(value);
        var expected = $"L'identifiant {value} n'est pas valide";

        // Act
        var result = rule.Message;

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GivenValidate_WhenInvalidId_ThenReturnFalse()
    {
        // Arrange
        var rule = new IdMustBeValidRule(Guid.Empty);

        // Act
        var result = rule.Validate();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void GivenValidate_WhenValidId_ThenReturnTrue()
    {
        // Arrange
        var rule = new IdMustBeValidRule(Guid.NewGuid());

        // Act
        var result = rule.Validate();

        // Assert
        Assert.True(result);
    }
}
