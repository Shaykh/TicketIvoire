using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicketIvoire.Administration.Domain.LieuEvenements.Events;
using TicketIvoire.Administration.Infrastructure.Persistence.LieuEvenements.EventHandlers;
using TicketIvoire.Administration.Infrastructure.Persistence;
using Moq;

namespace TicketIvoire.Administration.Infrastructure.Tests.Persistence.LieuEvenements.EventHandlers;

public class LieuAjouteEventHandlerTests
{
    [Fact]
    public async Task GivenLieuAjouteEventHandler_WhenHandleAsync_ThenLieuIsAdded()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
            .UseInMemoryDatabase(nameof(GivenLieuAjouteEventHandler_WhenHandleAsync_ThenLieuIsAdded))
            .Options;

        using var dbContext = new AdministrationDbContext(options);

        var handler = new LieuAjouteEventHandler(Mock.Of<ILogger<LieuAjouteEventHandler>>(), dbContext);
        var lieuEvent = new LieuAjouteEvent(Guid.NewGuid(), 100, "Nom", "Description", "Adresse", "Ville");

        // Act
        await handler.HandleAsync(lieuEvent, CancellationToken.None);
        await dbContext.SaveChangesAsync();

        // Assert
        var lieu = await dbContext.Lieux.SingleOrDefaultAsync(l => l.Id == lieuEvent.LieuId);
        Assert.NotNull(lieu);
        Assert.Equal(lieuEvent.Nom, lieu.Nom);
        Assert.Equal(lieuEvent.Adresse, lieu.Adresse);
        Assert.Equal(lieuEvent.Description, lieu.Description);
        Assert.Equal(lieuEvent.Ville, lieu.Ville);
        Assert.Equal(lieuEvent.Capacite, lieu.Capacite);
        Assert.Null(lieu.DeletedAt);
    }

    [Fact]
    public void GivenIsTransactional_WhenCalled_ThenReturnTrue()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
            .UseInMemoryDatabase(nameof(GivenIsTransactional_WhenCalled_ThenReturnTrue))
            .Options;

        using var dbContext = new AdministrationDbContext(options);
        var handler = new LieuAjouteEventHandler(Mock.Of<ILogger<LieuAjouteEventHandler>>(), dbContext);

        // Act
        var result = handler.IsTransactional();

        // Assert
        Assert.True(result);
    }
}
