using Microsoft.Extensions.Logging;
using Moq;
using TicketIvoire.Administration.Application.PropositionEvenements.Query;
using TicketIvoire.Administration.Domain.PropositionEvenements;

namespace TicketIvoire.Administration.Application.Tests.PropositionEvenements.Query;

public class GetNombreAllPropositionsQueryHandlerTests
{
    private static GetNombreAllPropositionsQueryHandler MakeSut(out Mock<IPropositionEvenementRepository> propositionEvenementRepositoryMock)
    {
        propositionEvenementRepositoryMock = new();

        return new GetNombreAllPropositionsQueryHandler(Mock.Of<ILogger<GetNombreAllPropositionsQueryHandler>>(), propositionEvenementRepositoryMock.Object);
    }

    [Fact]
    public async Task GivenHandle_WhenValidQuery_ThenReturnCorrectResponse()
    {
        // Arrange
        var query = new GetNombreAllPropositionsQuery();
        var handler = MakeSut(out var propositionEvenementRepositoryMock);
        propositionEvenementRepositoryMock.Setup(r => r.GetAllCountAsync(CancellationToken.None))
            .ReturnsAsync(10);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(10, result);
        propositionEvenementRepositoryMock.Verify(r => r.GetAllCountAsync(CancellationToken.None),
            Times.Once);
    }
}
