using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using TicketIvoire.Administration.Domain.PropositionEvenements.Events;
using TicketIvoire.Administration.Infrastructure.Persistence;
using TicketIvoire.Administration.Infrastructure.Persistence.PropositionEvenements.EventHandlers;

namespace TicketIvoire.Administration.Infrastructure.Tests.Persistence.PropositionEvenements.EventHandlers;

public class PropositionEvenementCreeEventHandlerTests
{
    [Fact]
    public async Task GivenMembreCreeEventHandler_WhenHandleAsync_ThenMembreIsAdded()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
            .UseInMemoryDatabase(nameof(GivenMembreCreeEventHandler_WhenHandleAsync_ThenMembreIsAdded))
            .Options;

        using var dbContext = new AdministrationDbContext(options);
        var handler = new PropositionEvenementCreeEventHandler(Mock.Of<ILogger<PropositionEvenementCreeEventHandler>>(), dbContext);
        var propositionEvent = new PropositionEvenementCreeEvent(Guid.NewGuid(), Guid.NewGuid(), "Nom", "Description", DateTime.UtcNow, DateTime.UtcNow.AddDays(1), 
            new Domain.PropositionEvenements.PropositionLieu("NomLieu", "DescriptionLieu", "VilleLieu", null));

        // Act
        await handler.HandleAsync(propositionEvent, CancellationToken.None);
        await dbContext.SaveChangesAsync();

        // Assert
        var proposition = await dbContext.PropositionEvenements.SingleOrDefaultAsync();
        Assert.NotNull(proposition);
        Assert.Null(proposition.DeletedAt);
        Assert.Equal(propositionEvent.Nom, proposition.Nom);
        Assert.Equal(propositionEvent.Description, proposition.Description);
        Assert.Equal(propositionEvent.DateFin, proposition.DateFin);
        Assert.Equal(propositionEvent.DateDebut, proposition.DateDebut);
        Assert.Equal(propositionEvent.PropositionEvenementId, proposition.Id);
        Assert.Equal(propositionEvent.UtilisateurId, proposition.UtilisateurId);
        Assert.Equal(propositionEvent.Lieu, proposition.Lieu);
    }

    [Fact]
    public void GivenIsTransactional_WhenCalled_ThenReturnTrue()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
            .UseInMemoryDatabase(nameof(GivenIsTransactional_WhenCalled_ThenReturnTrue))
            .Options;
        using var dbContext = new AdministrationDbContext(options);
        var handler = new PropositionEvenementCreeEventHandler(Mock.Of<ILogger<PropositionEvenementCreeEventHandler>>(), dbContext);

        // Act
        var result = handler.IsTransactional();

        // Assert
        Assert.True(result);
    }
}
