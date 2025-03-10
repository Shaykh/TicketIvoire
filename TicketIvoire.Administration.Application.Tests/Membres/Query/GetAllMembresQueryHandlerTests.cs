using Microsoft.Extensions.Logging;
using Moq;
using TicketIvoire.Administration.Application.Membres.Query;
using TicketIvoire.Administration.Domain.Membres;

namespace TicketIvoire.Administration.Application.Tests.Membres.Query;

public class GetAllMembresQueryHandlerTests
{
    private static GetAllQueryHandler MakeSut(out Mock<IMembreRepository> membreRepositoryMock)
    {
        membreRepositoryMock = new();
        return new GetAllQueryHandler(Mock.Of<ILogger<GetAllQueryHandler>>(), membreRepositoryMock.Object);
    }

    [Fact]
    public async Task GivenHandle_WhenQueryWithStatutAdhesionNull_ThenReturnCorrectResponse()
    {
        // Arrange
        var query = new GetAllMembresQuery(null, 1, 10);
        var handler = MakeSut(out var membreRepositoryMock);
        var membres = new List<Membre>
        {
            Membre.Create("login1", "email1@example.com", "Nom1", "Prenom1", "0123456789", DateTime.Now),
            Membre.Create("login2", "email2@example.com", "Nom2", "Prenom2", "0123456789", DateTime.Now)
        };
        membreRepositoryMock.Setup(r => r.GetAllAsync(query.PageNumber, query.NumberByPage))
            .ReturnsAsync(membres);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        membreRepositoryMock.Verify(r => r.GetAllAsync(query.PageNumber, query.NumberByPage), Times.Once);
        membreRepositoryMock.Verify(r => r.GetAllByStatutAdhesionAsync(It.IsAny<StatutAdhesion>(), query.PageNumber, query.NumberByPage), Times.Never);
    }

    [Fact]
    public async Task GivenHandle_WhenQueryWithStatutAdhesionAccepte_ThenReturnCorrectResponse()
    {
        // Arrange
        var query = new GetAllMembresQuery(StatutAdhesion.Accepte, 1, 10);
        var handler = MakeSut(out var membreRepositoryMock);
        var membres = new List<Membre>
        {
            Membre.Create("login1", "email1@example.com", "Nom1", "Prenom1", "0123456789", DateTime.Now),
            Membre.Create("login2", "email2@example.com", "Nom2", "Prenom2", "0123456789", DateTime.Now)
        };
        membreRepositoryMock.Setup(r => r.GetAllByStatutAdhesionAsync(query.StatutAdhesion!.Value, query.PageNumber, query.NumberByPage))
            .ReturnsAsync(membres);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        membreRepositoryMock.Verify(r => r.GetAllAsync(It.IsAny<uint?>(), It.IsAny<uint?>()), Times.Never);
        membreRepositoryMock.Verify(r => r.GetAllByStatutAdhesionAsync(query.StatutAdhesion!.Value, query.PageNumber, query.NumberByPage), Times.Once);
    }

    [Fact]
    public async Task GivenHandle_WhenQueryWithStatutAdhesionRefuse_ThenReturnCorrectResponse()
    {
        // Arrange
        var query = new GetAllMembresQuery(StatutAdhesion.Refuse, 1, 10);
        var handler = MakeSut(out var membreRepositoryMock);
        var membres = new List<Membre>
        {
            Membre.Create("login1", "email1@example.com", "Nom1", "Prenom1", "0123456789", DateTime.Now),
            Membre.Create("login2", "email2@example.com", "Nom2", "Prenom2", "0123456789", DateTime.Now)
        };
        membreRepositoryMock.Setup(r => r.GetAllByStatutAdhesionAsync(query.StatutAdhesion!.Value, query.PageNumber, query.NumberByPage))
            .ReturnsAsync(membres);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        membreRepositoryMock.Verify(r => r.GetAllAsync(It.IsAny<uint?>(), It.IsAny<uint?>()), Times.Never);
        membreRepositoryMock.Verify(r => r.GetAllByStatutAdhesionAsync(query.StatutAdhesion!.Value, query.PageNumber, query.NumberByPage), Times.Once);
    }

    [Fact]
    public async Task GivenHandle_WhenQueryWithStatutAdhesionEnAttente_ThenReturnCorrectResponse()
    {
        // Arrange
        var query = new GetAllMembresQuery(StatutAdhesion.EnAttente, 1, 10);
        var handler = MakeSut(out var membreRepositoryMock);
        var membres = new List<Membre>
        {
            Membre.Create("login1", "email1@example.com", "Nom1", "Prenom1", "0123456789", DateTime.Now),
            Membre.Create("login2", "email2@example.com", "Nom2", "Prenom2", "0123456789", DateTime.Now)
        };
        membreRepositoryMock.Setup(r => r.GetAllByStatutAdhesionAsync(query.StatutAdhesion!.Value, query.PageNumber, query.NumberByPage))
            .ReturnsAsync(membres);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        membreRepositoryMock.Verify(r => r.GetAllAsync(It.IsAny<uint?>(), It.IsAny<uint?>()), Times.Never);
        membreRepositoryMock.Verify(r => r.GetAllByStatutAdhesionAsync(query.StatutAdhesion!.Value, query.PageNumber, query.NumberByPage), Times.Once);
    }
}
