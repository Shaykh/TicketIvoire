using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using TicketIvoire.Administration.Domain.Membres.Events;
using TicketIvoire.Administration.Infrastructure.Persistence.Membres.EventHandlers;
using TicketIvoire.Administration.Infrastructure.Persistence;

namespace TicketIvoire.Administration.Infrastructure.Tests.Persistence.Membres.EventHandlers;

public class MembreCreeEventHandlerTests
{
    [Fact]
    public async Task GivenMembreCreeEventHandler_WhenHandleAsync_ThenMembreIsAdded()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
            .UseInMemoryDatabase(nameof(GivenMembreCreeEventHandler_WhenHandleAsync_ThenMembreIsAdded))
            .Options;

        using var dbContext = new AdministrationDbContext(options);
        var handler = new MembreCreeEventHandler(Mock.Of<ILogger<MembreCreeEventHandler>>(), dbContext);
        var membreEvent = new MembreCreeEvent(Guid.NewGuid(), "login", "email@example.com", "Nom", "Prenom", "0123456789", DateTime.Now);

        // Act
        await handler.HandleAsync(membreEvent, CancellationToken.None);
        await dbContext.SaveChangesAsync();

        // Assert
        var membre = await dbContext.Membres.SingleOrDefaultAsync(m => m.Id == membreEvent.MembreId);
        Assert.NotNull(membre);
        Assert.Equal(membreEvent.Login, membre.Login);
        Assert.Equal(membreEvent.Nom, membre.Nom);
        Assert.Equal(membreEvent.Prenom, membre.Prenom);
        Assert.Equal(membreEvent.Email, membre.Email);
        Assert.Equal(membreEvent.Telephone, membre.Telephone);
        Assert.Equal(membreEvent.MembreId, membre.Id);
        Assert.Equal(membreEvent.DateAdhesion, membre.DateAdhesion);
        Assert.Null(membre.DeletedAt);
    }

    [Fact]
    public void GivenIsTransactional_WhenCalled_ThenReturnTrue()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
            .UseInMemoryDatabase(nameof(GivenIsTransactional_WhenCalled_ThenReturnTrue))
            .Options;

        using var dbContext = new AdministrationDbContext(options);
        var handler = new MembreCreeEventHandler(Mock.Of<ILogger<MembreCreeEventHandler>>(), dbContext);

        // Act
        var result = handler.IsTransactional();

        // Assert
        Assert.True(result);
    }
}
