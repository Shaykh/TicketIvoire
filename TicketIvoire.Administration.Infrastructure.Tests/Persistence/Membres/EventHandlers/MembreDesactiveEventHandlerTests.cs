using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using TicketIvoire.Administration.Domain.Membres.Events;
using TicketIvoire.Administration.Infrastructure.Persistence.Membres.EventHandlers;
using TicketIvoire.Administration.Infrastructure.Persistence.Membres;
using TicketIvoire.Administration.Infrastructure.Persistence;
using TicketIvoire.Shared.Application.Exceptions;

namespace TicketIvoire.Administration.Infrastructure.Tests.Persistence.Membres.EventHandlers;

public class MembreDesactiveEventHandlerTests
{
    [Fact]
    public async Task GivenMembreDesactiveEventHandler_WhenExistingMembre_ThenMembreIsDeactivated()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
            .UseInMemoryDatabase(nameof(GivenMembreDesactiveEventHandler_WhenExistingMembre_ThenMembreIsDeactivated))
            .Options;

        using var dbContext = new AdministrationDbContext(options);
        var handler = new MembreDesactiveEventHandler(Mock.Of<ILogger<MembreDesactiveEventHandler>>(), dbContext);
        var membreId = Guid.NewGuid();
        var membre = new MembreEntity { Id = membreId, Login = "login", Email = "email@example.com", Nom = "Nom", Prenom = "Prenom", Telephone = "0123456789", DateAdhesion = DateTime.Now, EstActif = true };
        await dbContext.Membres.AddAsync(membre);
        await dbContext.SaveChangesAsync();
        var membreEvent = new MembreDesactiveEvent(membreId, "raisons");

        // Act
        await handler.HandleAsync(membreEvent, CancellationToken.None);

        // Assert
        var updatedMembre = await dbContext.Membres.SingleOrDefaultAsync(m => m.Id == membreId);
        Assert.NotNull(updatedMembre);
        Assert.False(updatedMembre.EstActif);
    }

    [Fact]
    public async Task GivenMembreDesactiveEventHandler_WhenNotFoundMembre_ThenThrowException()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
            .UseInMemoryDatabase(nameof(GivenMembreDesactiveEventHandler_WhenNotFoundMembre_ThenThrowException))
            .Options;

        using var dbContext = new AdministrationDbContext(options);
        var handler = new MembreDesactiveEventHandler(Mock.Of<ILogger<MembreDesactiveEventHandler>>(), dbContext);
        var membreId = Guid.NewGuid();
        var membreEvent = new MembreDesactiveEvent(membreId, "raisons");

        // Act
        async Task act() => await handler.HandleAsync(membreEvent, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(act);
    }

    [Fact]
    public void GivenIsTransactional_WhenCalled_ThenReturnTrue()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AdministrationDbContext>()
            .UseInMemoryDatabase(nameof(GivenIsTransactional_WhenCalled_ThenReturnTrue))
            .Options;

        using var dbContext = new AdministrationDbContext(options);
        var handler = new MembreDesactiveEventHandler(Mock.Of<ILogger<MembreDesactiveEventHandler>>(), dbContext);

        // Act
        var result = handler.IsTransactional();

        // Assert
        Assert.True(result);
    }
}
