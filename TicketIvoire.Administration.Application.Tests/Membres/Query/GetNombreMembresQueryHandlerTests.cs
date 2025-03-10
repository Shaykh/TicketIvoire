using Microsoft.Extensions.Logging;
using Moq;
using TicketIvoire.Administration.Application.Membres.Query;
using TicketIvoire.Administration.Domain.Membres;

namespace TicketIvoire.Administration.Application.Tests.Membres.Query;

public class GetNombreMembresQueryHandlerTests
{
    private static GetNombreMembresQueryHandler MakeSut(out Mock<IMembreRepository> membreRepositoryMock)
    {
        membreRepositoryMock = new();
        return new GetNombreMembresQueryHandler(Mock.Of<ILogger<GetNombreMembresQueryHandler>>(), membreRepositoryMock.Object);
    }

    [Fact]
    public async Task GivenHandle_WhenQueryWithStatutAdhesionNull_ThenReturnCorrectResponse()
    {
        // Arrange
        var query = new GetNombreMembresQuery(null);
        var handler = MakeSut(out var membreRepositoryMock);
        membreRepositoryMock.Setup(r => r.GetAllCountAsync())
            .ReturnsAsync(10);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(10, result);
        membreRepositoryMock.Verify(r => r.GetAllCountAsync(), Times.Once);
        membreRepositoryMock.Verify(r => r.GetCountByStatutAdhesionAsync(It.IsAny<StatutAdhesion>()), Times.Never);
    }

    [Fact]
    public async Task GivenHandle_WhenQueryWithStatutAdhesionEnAttente_ThenReturnCorrectResponse()
    {
        // Arrange
        var query = new GetNombreMembresQuery(StatutAdhesion.EnAttente);
        var handler = MakeSut(out var membreRepositoryMock);
        membreRepositoryMock.Setup(r => r.GetCountByStatutAdhesionAsync(StatutAdhesion.EnAttente))
            .ReturnsAsync(10);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(10, result);
        membreRepositoryMock.Verify(r => r.GetAllCountAsync(), Times.Never);
        membreRepositoryMock.Verify(r => r.GetCountByStatutAdhesionAsync(StatutAdhesion.EnAttente), Times.Once);
    }

    [Fact]
    public async Task GivenHandle_WhenQueryWithStatutAdhesionAccepte_ThenReturnCorrectResponse()
    {
        // Arrange
        var query = new GetNombreMembresQuery(StatutAdhesion.Accepte);
        var handler = MakeSut(out var membreRepositoryMock);
        membreRepositoryMock.Setup(r => r.GetCountByStatutAdhesionAsync(StatutAdhesion.Accepte))
            .ReturnsAsync(10);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(10, result);
        membreRepositoryMock.Verify(r => r.GetAllCountAsync(), Times.Never);
        membreRepositoryMock.Verify(r => r.GetCountByStatutAdhesionAsync(StatutAdhesion.Accepte), Times.Once);
    }

    [Fact]
    public async Task GivenHandle_WhenQueryWithStatutAdhesionRefuse_ThenReturnCorrectResponse()
    {
        // Arrange
        var query = new GetNombreMembresQuery(StatutAdhesion.Refuse);
        var handler = MakeSut(out var membreRepositoryMock);
        membreRepositoryMock.Setup(r => r.GetCountByStatutAdhesionAsync(StatutAdhesion.Refuse))
            .ReturnsAsync(10);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(10, result);
        membreRepositoryMock.Verify(r => r.GetAllCountAsync(), Times.Never);
        membreRepositoryMock.Verify(r => r.GetCountByStatutAdhesionAsync(StatutAdhesion.Refuse), Times.Once);
    }
}
