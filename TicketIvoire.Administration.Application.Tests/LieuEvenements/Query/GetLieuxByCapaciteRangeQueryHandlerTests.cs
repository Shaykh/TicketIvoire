using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using TicketIvoire.Administration.Application.LieuEvenements.Query;
using TicketIvoire.Administration.Domain.LieuEvenements;

namespace TicketIvoire.Administration.Application.Tests.LieuEvenements.Query;

public class GetLieuxByCapaciteRangeQueryHandlerTests
{
    private static GetLieuxByCapaciteRangeQueryHandler MakeSut(out Mock<ILieuRepository> lieuRepositoryMock)
    {
        var validator = new GetLieuxByCapaciteRangeQueryValidator();
        lieuRepositoryMock = new();

        return new GetLieuxByCapaciteRangeQueryHandler(Mock.Of<ILogger<GetLieuxByCapaciteRangeQueryHandler>>(), validator, lieuRepositoryMock.Object);
    }

    [Fact]
    public async Task GivenHandle_WhenValidQuery_ThenReturnCorrectResponse()
    {
        // Arrange
        var query = new GetLieuxByCapaciteRangeQuery(100, 500);
        var handler = MakeSut(out var lieuRepositoryMock);
        var lieux = new List<Lieu>
        {
            Lieu.Create("nom1", "description1", "adresse1", "ville1", 200),
            Lieu.Create("nom2", "description2", "adresse2", "ville2", 300)
        };
        lieuRepositoryMock.Setup(r => r.GetAllByCapaciteRangeAsync(query.Minimum, query.Maximum))
            .ReturnsAsync(lieux);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal("nom1", result.First().Nom);
        Assert.Equal("nom2", result.Last().Nom);
        lieuRepositoryMock.Verify(r => r.GetAllByCapaciteRangeAsync(query.Minimum, query.Maximum),
            Times.Once);
    }

    [Fact]
    public async Task GivenHandle_WhenInvalidQuery_ThenThrowException()
    {
        // Arrange
        var query = new GetLieuxByCapaciteRangeQuery(500, 100);
        var handler = MakeSut(out var lieuRepositoryMock);

        // Act
        async Task act() => await handler.Handle(query, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<ValidationException>(act);
        lieuRepositoryMock.Verify(r => r.GetAllByCapaciteRangeAsync(It.IsAny<uint?>(), It.IsAny<uint?>()),
            Times.Never);
    }
}
