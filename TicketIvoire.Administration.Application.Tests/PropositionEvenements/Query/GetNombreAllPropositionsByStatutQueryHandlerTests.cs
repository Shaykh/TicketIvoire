using Microsoft.Extensions.Logging;
using Moq;
using TicketIvoire.Administration.Application.PropositionEvenements.Query;
using TicketIvoire.Administration.Domain.PropositionEvenements;

namespace TicketIvoire.Administration.Application.Tests.PropositionEvenements.Query;

public class GetNombreAllPropositionsByStatutQueryHandlerTests
{
    private static GetNombreAllPropositionsByStatutQueryHandler MakeSut(out Mock<IPropositionEvenementRepository> propositionEvenementRepositoryMock)
    {
        propositionEvenementRepositoryMock = new();

        return new GetNombreAllPropositionsByStatutQueryHandler(Mock.Of<ILogger<GetNombreAllPropositionsByStatutQueryHandler>>(), propositionEvenementRepositoryMock.Object);
    }

    [Fact]
    public async Task GivenHandle_WhenQuery_ThenReturnCorrectResponse()
    {
        // Arrange
        var query = new GetNombreAllPropositionsByStatutQuery(PropositionStatut.AVerifier);
        var handler = MakeSut(out var propositionEvenementRepositoryMock);
        propositionEvenementRepositoryMock.Setup(r => r.GetAllCountByStatutAsync(query.Statut, CancellationToken.None))
            .ReturnsAsync(10);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(10, result);
        propositionEvenementRepositoryMock.Verify(r => r.GetAllCountByStatutAsync(query.Statut, CancellationToken.None),
            Times.Once);
    }
}
