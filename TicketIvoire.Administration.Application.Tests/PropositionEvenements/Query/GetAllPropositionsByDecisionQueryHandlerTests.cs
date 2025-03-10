using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using TicketIvoire.Administration.Application.PropositionEvenements.Query;
using TicketIvoire.Administration.Domain.PropositionEvenements;
using TicketIvoire.Administration.Domain.Utilisateurs;

namespace TicketIvoire.Administration.Application.Tests.PropositionEvenements.Query;

public class GetAllPropositionsByDecisionQueryHandlerTests
{
    private static GetAllPropositionsByDecisionQueryHandler MakeSut(out Mock<IPropositionEvenementRepository> propositionEvenementRepositoryMock)
    {
        var validator = new GetAllPropositionsByDecisionQueryValidator();
        propositionEvenementRepositoryMock = new();

        return new GetAllPropositionsByDecisionQueryHandler(Mock.Of<ILogger<GetAllPropositionsByDecisionQueryHandler>>(), validator, propositionEvenementRepositoryMock.Object);
    }

    [Fact]
    public async Task GivenHandle_WhenValidQuery_ThenReturnCorrectResponse()
    {
        // Arrange
        var query = new GetAllPropositionsByDecisionQuery("Accepter", null, null);
        var handler = MakeSut(out var propositionEvenementRepositoryMock);
        var propositions = new List<PropositionEvenement>
        {
            PropositionEvenement.Create(new UtilisateurId(Guid.NewGuid()), "nom1", "description1", DateTime.Now.AddDays(-1), DateTime.Now, new PropositionLieu("Lieu1", "Adresse1", "Ville1", null)),
            PropositionEvenement.Create(new UtilisateurId(Guid.NewGuid()), "nom2", "description2", DateTime.Now.AddDays(-1), DateTime.Now, new PropositionLieu("Lieu2", "Adresse2", "Ville2", null))
        };
        propositionEvenementRepositoryMock.Setup(r => r.GetAllByDecisionCodeAsync(query.DecisionCode, null, null))
            .ReturnsAsync(propositions);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal("nom1", result.First().Nom);
        Assert.Equal("nom2", result.Last().Nom);
        propositionEvenementRepositoryMock.Verify(r => r.GetAllByDecisionCodeAsync(query.DecisionCode, query.PageNumber, query.NumberByPage),
            Times.Once);
    }

    [Fact]
    public async Task GivenHandle_WhenInvalidQuery_ThenThrowException()
    {
        // Arrange
        var query = new GetAllPropositionsByDecisionQuery("", null, null);
        var handler = MakeSut(out var propositionEvenementRepositoryMock);

        // Act
        async Task act() => await handler.Handle(query, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<ValidationException>(act);
        propositionEvenementRepositoryMock.Verify(r => r.GetAllByDecisionCodeAsync(It.IsAny<string>(), It.IsAny<uint?>(), It.IsAny<uint?>()),
            Times.Never);
    }
}
