using TicketIvoire.Administration.Domain.PropositionEvenements;

namespace TicketIvoire.Administration.Domain.Tests.PropositionEvenements;

public class PropositionDecisionTests
{
    [Fact]
    public void GivenPropositionAcceptee_WhenCalled_ThenReturnPropositionWithCodeAccepte()
    {
        // Arrange
        (Guid utilisateurId, DateTime dateDecision) = (Guid.NewGuid(), DateTime.Now);

        // Act
        var decision = PropositionDecision.PropositionAcceptee(utilisateurId, dateDecision);

        // Assert
        Assert.NotNull(decision);
        Assert.Equal(utilisateurId, decision.UtilisateurId.Value);
        Assert.Equal(dateDecision, decision.DateDecision);
        Assert.Equal(PropositionDecision.AccepterCode, decision.Code);
        Assert.Null(decision.Raisons);
    }

    [Fact]
    public void GivenPropositionRefusee_WhenCalled_ThenReturnPropositionWithCodeRefuse()
    {
        // Arrange
        (Guid utilisateurId, DateTime dateDecision, string raisons) = (Guid.NewGuid(), DateTime.Now, "raisons");

        // Act
        var decision = PropositionDecision.PropositionRefusee(utilisateurId, dateDecision, raisons);

        // Assert
        Assert.NotNull(decision);
        Assert.Equal(utilisateurId, decision.UtilisateurId.Value);
        Assert.Equal(dateDecision, decision.DateDecision);
        Assert.Equal(PropositionDecision.RefuserCode, decision.Code);
        Assert.Equal(raisons, decision.Raisons);
    }
}
