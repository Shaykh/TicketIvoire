using TicketIvoire.Administration.Domain.Membres.Rules;

namespace TicketIvoire.Administration.Domain.Tests.Membres.Rules;

public class LoginMustBeValidRuleTests
{
    [Fact]
    public void GivenMessage_WhenCalled_ThenReturnCorrectText()
    {
        // Arrange
        var login = "login";
        var rule = new LoginMustBeValidRule(login);
        var expected = $"Le login doit avoir entre 3 et 50 caractères. {login} n'est donc pas valide";

        // Act
        var result = rule.Message;

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GivenValidate_WhenValidLogin_ThenReturnTrue()
    {
        // Arrange
        var login = "login";
        var rule = new LoginMustBeValidRule(login);

        // Act
        var result = rule.Validate();

        // Assert
        Assert.True(result);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("1")]
    [InlineData("12")]
    [InlineData("GivenValidate_WhenValidLogin_ThenReturnFalseGivenValidate_WhenValidLogin_ThenReturnFalse")]
    public void GivenValidate_WhenValidLogin_ThenReturnFalse(string login)
    {
        // Arrange
        var rule = new LoginMustBeValidRule(login);

        // Act
        var result = rule.Validate();

        // Assert
        Assert.False(result);
    }
}
