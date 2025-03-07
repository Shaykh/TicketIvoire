using Moq;
using TicketIvoire.Shared.Domain.BusinessRules;
using TicketIvoire.Shared.Domain.Exceptions;

namespace TicketIvoire.Shared.Domain.Tests;

public class EntityBaseTests
{
    [Fact]
    public void GivenCheckRule_WhenRuleValidate_ThenDoNotThrowException()
    {
        // Arrange
        var ruleMock = new Mock<IBusinessRule>();
        ruleMock.Setup(r => r.Validate()).Returns(true);

        // Act
        EntityBase.CheckRule(ruleMock.Object);

        // Assert
        ruleMock.Verify(r => r.Validate(), Times.Once);
    }

    [Fact]
    public void GivenCheckRule_WhenRuleNotValidate_ThenThrowException()
    {
        // Arrange
        var ruleMock = new Mock<IBusinessRule>();
        ruleMock.Setup(r => r.Validate()).Returns(false);

        // Act
        void act() => EntityBase.CheckRule(ruleMock.Object);

        // Assert
        Assert.Throws<BrokenBusinessRuleException>(act);
        ruleMock.Verify(r => r.Validate(), Times.Once);
    }
}
