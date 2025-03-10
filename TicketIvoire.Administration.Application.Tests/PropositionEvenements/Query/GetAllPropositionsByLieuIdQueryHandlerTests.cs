using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using TicketIvoire.Administration.Application.PropositionEvenements.Query;
using TicketIvoire.Administration.Domain.PropositionEvenements;
using TicketIvoire.Administration.Domain.Utilisateurs;

namespace TicketIvoire.Administration.Application.Tests.PropositionEvenements.Query;

public class GetAllPropositionsByLieuIdQueryHandlerTests
{
    private static GetAllPropositionsByLieuIdQueryHandler MakeSut(out Mock<IPropositionEvenementRepository> propositionEvenementRepositoryMock)
    {
        var validator = new GetAllPropositionsByLieuIdQueryValidator();
        propositionEvenementRepositoryMock = new();

        return new GetAllPropositionsByLieuIdQueryHandler(Mock.Of<ILogger<GetAllPropositionsByLieuIdQueryHandler>>(), validator, propositionEvenementRepositoryMock.Object);
    }

    [Fact]
    public async Task GivenHandle_WhenValidQuery_ThenReturnCorrectResponse()
    {
        // Arrange
        var query = new GetAllPropositionsByLieuIdQuery(Guid.NewGuid());
        var handler = MakeSut(out var propositionEvenementRepositoryMock);
        var propositions = new List<PropositionEvenement>
        {
            PropositionEvenement.Create(new UtilisateurId(Guid.NewGuid()), "nom1", "description1", DateTime.Now.AddDays(-1), DateTime.Now, new PropositionLieu("Lieu1", "Adresse1", "Ville1", null)),
            PropositionEvenement.Create(new UtilisateurId(Guid.NewGuid()), "nom2", "description2", DateTime.Now.AddDays(-1), DateTime.Now, new PropositionLieu("Lieu2", "Adresse2", "Ville2", null))
        };
        propositionEvenementRepositoryMock.Setup(r => r.GetAllByLieuIdAsync(query.LieuId))
            .ReturnsAsync(propositions);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal("nom1", result.First().Nom);
        Assert.Equal("nom2", result.Last().Nom);
        propositionEvenementRepositoryMock.Verify(r => r.GetAllByLieuIdAsync(query.LieuId),
            Times.Once);
    }

    [Fact]
    public async Task GivenHandle_WhenInvalidQuery_ThenThrowException()
    {
        // Arrange
        var query = new GetAllPropositionsByLieuIdQuery(Guid.Empty);
        var handler = MakeSut(out var propositionEvenementRepositoryMock);

        // Act
        async Task act() => await handler.Handle(query, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<ValidationException>(act);
        propositionEvenementRepositoryMock.Verify(r => r.GetAllByLieuIdAsync(It.IsAny<Guid>()),
            Times.Never);
    }
}
