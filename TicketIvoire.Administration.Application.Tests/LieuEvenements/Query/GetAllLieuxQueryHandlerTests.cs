using Microsoft.Extensions.Logging;
using Moq;
using TicketIvoire.Administration.Application.LieuEvenements.Query;
using TicketIvoire.Administration.Domain.LieuEvenements;

namespace TicketIvoire.Administration.Application.Tests.LieuEvenements.Query;

public class GetAllLieuxQueryHandlerTests
{
    private static GetAllLieuxQueryHandler MakeSut(out Mock<ILieuRepository> lieuRepositoryMock)
    {
        lieuRepositoryMock = new();

        return new GetAllLieuxQueryHandler(Mock.Of<ILogger<GetAllLieuxQueryHandler>>(), lieuRepositoryMock.Object);
    }

    [Fact]
    public async Task GivenHandle_WhenQuery_ThenReturnCorrectResponse()
    {
        // Arrange
        var query = new GetAllLieuxQuery(1, 10);
        var handler = MakeSut(out var lieuRepositoryMock);
        var lieux = new List<Lieu>
        {
            Lieu.Create("nom1", "description1", "adresse1", "ville1", 200),
            Lieu.Create("nom2", "description2", "adresse2", "ville2", 300)
        };
        lieuRepositoryMock.Setup(r => r.GetAllAsync(1, 10, CancellationToken.None))
            .ReturnsAsync(lieux);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal("nom1", result.First().Nom);
        Assert.Equal("nom2", result.Last().Nom);
        lieuRepositoryMock.Verify(r => r.GetAllAsync(query.PageNumber, query.NumberByPage, CancellationToken.None),
            Times.Once);
    }
}
