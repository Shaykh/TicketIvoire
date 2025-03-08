using TicketIvoire.Administration.Domain.PropositionEvenements.Rules;

namespace TicketIvoire.Administration.Domain.Tests.PropositionEvenements.Rules;

public class DatesMustBeValidRuleTests
{
    [Fact]
    public void GivenMessage_WhenCalled_ThenReturnCorrectText()
    {
        // Arrange
        var dateDebut = DateTime.Now;
        var dateFin = dateDebut.AddDays(1);
        var rule = new DatesMustBeValidRule(dateDebut, dateFin);
        var expected = "La date de début doit être antérieure à la date de fin.";

        // Act
        var result = rule.Message;

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GivenValidate_WhenValidDates_ThenReturnTrue()
    {
        // Arrange
        var dateDebut = DateTime.Now;
        var dateFin = dateDebut.AddDays(1);
        var rule = new DatesMustBeValidRule(dateDebut, dateFin);

        // Act
        var result = rule.Validate();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void GivenValidate_WhenInvalidDates_ThenReturnFalse()
    {
        // Arrange
        var dateDebut = DateTime.Now;
        var dateFin = dateDebut.AddDays(-1);
        var rule = new DatesMustBeValidRule(dateDebut, dateFin);

        // Act
        var result = rule.Validate();

        // Assert
        Assert.False(result);
    }
}
