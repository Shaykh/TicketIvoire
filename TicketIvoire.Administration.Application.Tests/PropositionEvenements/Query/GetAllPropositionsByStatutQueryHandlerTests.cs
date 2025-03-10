using Microsoft.Extensions.Logging;
using Moq;
using TicketIvoire.Administration.Application.PropositionEvenements.Query;
using TicketIvoire.Administration.Domain.PropositionEvenements;
using TicketIvoire.Administration.Domain.Utilisateurs;

namespace TicketIvoire.Administration.Application.Tests.PropositionEvenements.Query;

public class GetAllPropositionsByStatutQueryHandlerTests
{
    private static GetAllPropositionsByStatutQueryHandler MakeSut(out Mock<IPropositionEvenementRepository> propositionEvenementRepositoryMock)
    {
        propositionEvenementRepositoryMock = new();

        return new GetAllPropositionsByStatutQueryHandler(Mock.Of<ILogger<GetAllPropositionsByStatutQueryHandler>>(), propositionEvenementRepositoryMock.Object);
    }

    [Fact]
    public async Task GivenHandle_WhenQuery_ThenReturnCorrectResponse()
    {
        // Arrange
        var query = new GetAllPropositionsByStatutQuery(PropositionStatut.AVerifier, null, null);
        var handler = MakeSut(out var propositionEvenementRepositoryMock);
        var propositions = new List<PropositionEvenement>
        {
            PropositionEvenement.Create(new UtilisateurId(Guid.NewGuid()), "nom1", "description1", DateTime.Now.AddDays(-1), DateTime.Now, new PropositionLieu("Lieu1", "Adresse1", "Ville1", null)),
            PropositionEvenement.Create(new UtilisateurId(Guid.NewGuid()), "nom2", "description2", DateTime.Now.AddDays(-1), DateTime.Now, new PropositionLieu("Lieu2", "Adresse2", "Ville2", null))
        };
        propositionEvenementRepositoryMock.Setup(r => r.GetAllByStatutAsync(query.Statut, null, null))
            .ReturnsAsync(propositions);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal("nom1", result.First().Nom);
        Assert.Equal("nom2", result.Last().Nom);
        propositionEvenementRepositoryMock.Verify(r => r.GetAllByStatutAsync(query.Statut, query.PageNumber, query.NumberByPage),
            Times.Once);
    }
}
