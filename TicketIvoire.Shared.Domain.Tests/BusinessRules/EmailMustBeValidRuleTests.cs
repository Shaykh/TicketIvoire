using TicketIvoire.Shared.Domain.BusinessRules;

namespace TicketIvoire.Shared.Domain.Tests.BusinessRules;

public class EmailMustBeValidRuleTests
{
    [Fact]
    public void GivenMessage_WhenCalled_ThenReturnCorrectText()
    {
        // Arrange
        var email = "test@example.com";
        var rule = new EmailMustBeValidRule(email);
        var expected = $"L'email {email} n'est pas valide";

        // Act
        var result = rule.Message;

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("invalid-email")]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("@")]
    [InlineData("test@.com")]
    [InlineData("@example.com")]
    [InlineData("test@.")]
    public void GivenValidate_WhenInvalidEmail_ThenReturnFalse(string email)
    {
        // Arrange
        var rule = new EmailMustBeValidRule(email);

        // Act
        var result = rule.Validate();

        // Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData("test@example.com")]
    [InlineData("test.nom@example.com")]
    [InlineData("test@example.domain.com")]
    public void GivenValidate_WhenValidEmail_ThenReturnTrue(string email)
    {
        // Arrange
        var rule = new EmailMustBeValidRule(email);

        // Act
        var result = rule.Validate();

        // Assert
        Assert.True(result);
    }
}
