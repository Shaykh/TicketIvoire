using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using TicketIvoire.Administration.Application.PropositionEvenements.Query;
using TicketIvoire.Administration.Domain.PropositionEvenements;

namespace TicketIvoire.Administration.Application.Tests.PropositionEvenements.Query;

public class GetNombreAllPropositionsByDecisionQueryHandlerTests
{
    private static GetNombreAllPropositionsByDecisionQueryHandler MakeSut(out Mock<IPropositionEvenementRepository> propositionEvenementRepositoryMock)
    {
        var validator = new GetNombreAllPropositionsByDecisionQueryValidator();
        propositionEvenementRepositoryMock = new();

        return new GetNombreAllPropositionsByDecisionQueryHandler(Mock.Of<ILogger<GetNombreAllPropositionsByDecisionQueryHandler>>(), validator, propositionEvenementRepositoryMock.Object);
    }

    [Fact]
    public async Task GivenHandle_WhenValidQuery_ThenReturnCorrectResponse()
    {
        // Arrange
        var query = new GetNombreAllPropositionsByDecisionQuery("Accepter");
        var handler = MakeSut(out var propositionEvenementRepositoryMock);
        propositionEvenementRepositoryMock.Setup(r => r.GetAllCountByDecisionCodeAsync(query.DecisionCode))
            .ReturnsAsync(10);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(10, result);
        propositionEvenementRepositoryMock.Verify(r => r.GetAllCountByDecisionCodeAsync(query.DecisionCode),
            Times.Once);
    }

    [Fact]
    public async Task GivenHandle_WhenInvalidQuery_ThenThrowException()
    {
        // Arrange
        var query = new GetNombreAllPropositionsByDecisionQuery("");
        var handler = MakeSut(out var propositionEvenementRepositoryMock);

        // Act
        async Task act() => await handler.Handle(query, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<ValidationException>(act);
        propositionEvenementRepositoryMock.Verify(r => r.GetAllCountByDecisionCodeAsync(It.IsAny<string>()),
            Times.Never);
    }
}
