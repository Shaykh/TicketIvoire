using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using TicketIvoire.Administration.Application.LieuEvenements.Query;
using TicketIvoire.Administration.Domain.LieuEvenements;

namespace TicketIvoire.Administration.Application.Tests.LieuEvenements.Query;

public class GetLieuByIdQueryHandlerTests
{
    private static GetLieuByIdQueryHandler MakeSut(out Mock<ILieuRepository> lieuRepositoryMock)
    {
        var validator = new GetLieuByIdQueryValidator();
        lieuRepositoryMock = new();

        return new GetLieuByIdQueryHandler(Mock.Of<ILogger<GetLieuByIdQueryHandler>>(), validator, lieuRepositoryMock.Object);
    }

    [Fact]
    public async Task GivenHandle_WhenValidQuery_ThenReturnCorrectResponse()
    {
        // Arrange
        var query = new GetLieuByIdQuery(Guid.NewGuid());
        var handler = MakeSut(out var lieuRepositoryMock);
        var lieu = Lieu.Create("nom", "description", "adresse", "ville", 455);
        var coordonneesGeo = new LieuCoordonneesGeographiques(-1.536m, -7.3654m);
        lieu.DefinirCoordonneesGeographiques(coordonneesGeo);
        lieuRepositoryMock.Setup(r => r.GetByIdAsync(query.Id, CancellationToken.None))
            .ReturnsAsync(lieu);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(lieu.Nom, result.Nom);
        Assert.Equal(lieu.Description, result.Description);
        Assert.Equal(lieu.Ville, result.Ville);
        Assert.Equal(lieu.Adresse, result.Adresse);
        Assert.Equal(coordonneesGeo.Latitude, result.CoordonneesGeo!.Latitude);
        Assert.Equal(coordonneesGeo.Longitude, result.CoordonneesGeo!.Longitude);
        lieuRepositoryMock.Verify(r => r.GetByIdAsync(query.Id, CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task GivenHandle_WhenValidQuery_ThenThrowException()
    {
        // Arrange
        var query = new GetLieuByIdQuery(Guid.Empty);
        var handler = MakeSut(out var lieuRepositoryMock);

        // Act
        async Task act() => await handler.Handle(query, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<ValidationException>(act);
        lieuRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }
}
