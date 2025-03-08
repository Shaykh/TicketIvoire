using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using TicketIvoire.Administration.Application.LieuEvenements.Query;
using TicketIvoire.Administration.Domain.LieuEvenements;

namespace TicketIvoire.Administration.Application.Tests.LieuEvenements.Query;

public class GetAllLieuxByVilleQueryHandlerTests
{
    private static GetAllLieuxByVilleQueryHandler MakeSut(out Mock<ILieuRepository> lieuRepositoryMock)
    {
        var validator = new GetAllLieuxByVilleQueryValidator();
        lieuRepositoryMock = new();

        return new GetAllLieuxByVilleQueryHandler(Mock.Of<ILogger<GetAllLieuxByVilleQueryHandler>>(), validator, lieuRepositoryMock.Object);
    }

    [Fact]
    public async Task GivenHandle_WhenValidQuery_ThenReturnCorrectResponse()
    {
        // Arrange
        var query = new GetAllLieuxByVilleQuery("ville1");
        var handler = MakeSut(out var lieuRepositoryMock);
        var lieux = new List<Lieu>
        {
            Lieu.Create("nom1", "description1", "adresse1", "ville1", 200),
            Lieu.Create("nom2", "description2", "adresse2", "ville1", 300)
        };
        lieuRepositoryMock.Setup(r => r.GetAllByVilleAsync(query.Ville))
            .ReturnsAsync(lieux);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal("nom1", result.First().Nom);
        Assert.Equal("nom2", result.Last().Nom);
    }

    [Fact]
    public async Task GivenHandle_WhenInvalidQuery_ThenThrowException()
    {
        // Arrange
        var query = new GetAllLieuxByVilleQuery("");
        var handler = MakeSut(out var lieuRepositoryMock);

        // Act
        async Task act() => await handler.Handle(query, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<ValidationException>(act);
        lieuRepositoryMock.Verify(r => r.GetAllByVilleAsync(It.IsAny<string>()),
            Times.Never);
    }
}
