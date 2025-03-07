using TicketIvoire.Shared.Domain.BusinessRules;

namespace TicketIvoire.Shared.Domain.Tests.BusinessRules;

public class TelephoneMustBeValidRuleTests
{
    [Theory]
    [InlineData("A123456789")]
    [InlineData("123456789")]
    [InlineData("071234567")]
    [InlineData("+224 0712345678")]
    [InlineData("0033 0512345678")]
    [InlineData("+16 0212345678")]
    [InlineData("00 243 07 12 34 56 78")]
    public void GivenValidate_WhenInvalidPhoneNumber_ThenReturnFalse(string phoneNumber)
    {
        // Arrange
        var rule = new TelephoneMustBeValidRule(phoneNumber);

        // Act
        var result = rule.Validate();

        // Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData("0123456789")]
    [InlineData("+ 2250123456789")]
    [InlineData("07 12 34 56 78")]
    [InlineData("+225 07 12 34 56 78")]
    [InlineData("21.45.78.90.12")]
    [InlineData("05-98-76-54-32")]
    [InlineData("00 225 07 12 34 56 78")]
    public void GivenValidate_WhenValidPhoneNumber_ThenReturnTrue(string phoneNumber)
    {
        // Arrange
        var rule = new TelephoneMustBeValidRule(phoneNumber);

        // Act
        var result = rule.Validate();

        // Assert
        Assert.True(result);
    }
}
