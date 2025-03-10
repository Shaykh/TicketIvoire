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
        membreRepositoryMock.Setup(r => r.GetAllCountAsync(CancellationToken.None))
            .ReturnsAsync(10);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(10, result);
        membreRepositoryMock.Verify(r => r.GetAllCountAsync(CancellationToken.None), Times.Once);
        membreRepositoryMock.Verify(r => r.GetCountByStatutAdhesionAsync(It.IsAny<StatutAdhesion>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task GivenHandle_WhenQueryWithStatutAdhesionEnAttente_ThenReturnCorrectResponse()
    {
        // Arrange
        var query = new GetNombreMembresQuery(StatutAdhesion.EnAttente);
        var handler = MakeSut(out var membreRepositoryMock);
        membreRepositoryMock.Setup(r => r.GetCountByStatutAdhesionAsync(StatutAdhesion.EnAttente, CancellationToken.None))
            .ReturnsAsync(10);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(10, result);
        membreRepositoryMock.Verify(r => r.GetAllCountAsync(It.IsAny<CancellationToken>()), Times.Never);
        membreRepositoryMock.Verify(r => r.GetCountByStatutAdhesionAsync(StatutAdhesion.EnAttente, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GivenHandle_WhenQueryWithStatutAdhesionAccepte_ThenReturnCorrectResponse()
    {
        // Arrange
        var query = new GetNombreMembresQuery(StatutAdhesion.Accepte);
        var handler = MakeSut(out var membreRepositoryMock);
        membreRepositoryMock.Setup(r => r.GetCountByStatutAdhesionAsync(StatutAdhesion.Accepte, CancellationToken.None))
            .ReturnsAsync(10);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(10, result);
        membreRepositoryMock.Verify(r => r.GetAllCountAsync(It.IsAny<CancellationToken>()), Times.Never);
        membreRepositoryMock.Verify(r => r.GetCountByStatutAdhesionAsync(StatutAdhesion.Accepte, CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GivenHandle_WhenQueryWithStatutAdhesionRefuse_ThenReturnCorrectResponse()
    {
        // Arrange
        var query = new GetNombreMembresQuery(StatutAdhesion.Refuse);
        var handler = MakeSut(out var membreRepositoryMock);
        membreRepositoryMock.Setup(r => r.GetCountByStatutAdhesionAsync(StatutAdhesion.Refuse, CancellationToken.None))
            .ReturnsAsync(10);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(10, result);
        membreRepositoryMock.Verify(r => r.GetAllCountAsync(It.IsAny<CancellationToken>()), Times.Never);
        membreRepositoryMock.Verify(r => r.GetCountByStatutAdhesionAsync(StatutAdhesion.Refuse, CancellationToken.None), Times.Once);
    }
}
