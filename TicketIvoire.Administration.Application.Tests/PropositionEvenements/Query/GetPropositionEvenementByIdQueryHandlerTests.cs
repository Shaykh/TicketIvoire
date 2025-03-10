using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using TicketIvoire.Administration.Application.PropositionEvenements.Query;
using TicketIvoire.Administration.Domain.PropositionEvenements;
using TicketIvoire.Administration.Domain.Utilisateurs;

namespace TicketIvoire.Administration.Application.Tests.PropositionEvenements.Query;

public class GetPropositionEvenementByIdQueryHandlerTests
{
    private static GetPropositionEvenementByIdQueryHandler MakeSut(out Mock<IPropositionEvenementRepository> propositionEvenementRepositoryMock)
    {
        var validator = new GetPropositionEvenementByIdQueryValidator();
        propositionEvenementRepositoryMock = new();

        return new GetPropositionEvenementByIdQueryHandler(Mock.Of<ILogger<GetPropositionEvenementByIdQueryHandler>>(), validator, propositionEvenementRepositoryMock.Object);
    }

    [Fact]
    public async Task GivenHandle_WhenValidQuery_ThenReturnCorrectResponse()
    {
        // Arrange
        var query = new GetPropositionEvenementByIdQuery(Guid.NewGuid());
        var handler = MakeSut(out var propositionEvenementRepositoryMock);
        var proposition = PropositionEvenement.Create(new UtilisateurId(Guid.NewGuid()), "nom", "description", DateTime.Now.AddDays(-1), DateTime.Now, new PropositionLieu("Lieu", "Adresse", "Ville", null));
        propositionEvenementRepositoryMock.Setup(r => r.GetByIdAsync(new PropositionEvenementId(query.Id), CancellationToken.None))
            .ReturnsAsync(proposition);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("nom", result.Nom);
        propositionEvenementRepositoryMock.Verify(r => r.GetByIdAsync(new PropositionEvenementId(query.Id), CancellationToken.None),
            Times.Once);
    }

    [Fact]
    public async Task GivenHandle_WhenInvalidQuery_ThenThrowException()
    {
        // Arrange
        var query = new GetPropositionEvenementByIdQuery(Guid.Empty);
        var handler = MakeSut(out var propositionEvenementRepositoryMock);

        // Act
        async Task act() => await handler.Handle(query, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<ValidationException>(act);
        propositionEvenementRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<PropositionEvenementId>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }
}
