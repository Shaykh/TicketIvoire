using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using TicketIvoire.Administration.Application.Membres.Query;
using TicketIvoire.Administration.Domain.Membres;

namespace TicketIvoire.Administration.Application.Tests.Membres.Query;

public class GetMembreByIdQueryHandlerTests
{
    private static GetMembreByIdQueryHandler MakeSut(out Mock<IMembreRepository> membreRepositoryMock)
    {
        var validator = new GetMembreByIdQueryValidator();
        membreRepositoryMock = new();

        return new GetMembreByIdQueryHandler(Mock.Of<ILogger<GetMembreByIdQueryHandler>>(), validator, membreRepositoryMock.Object);
    }

    [Fact]
    public async Task GivenHandle_WhenValidQuery_ThenReturnCorrectResponse()
    {
        // Arrange
        var query = new GetMembreByIdQuery(Guid.NewGuid());
        var handler = MakeSut(out var membreRepositoryMock);
        var membre = Membre.Create("login", "email@example.com", "Nom", "Prenom", "0123456789", DateTime.Now);
        membreRepositoryMock.Setup(r => r.GetByIdAsync(new MembreId(query.Id)))
            .ReturnsAsync(membre);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(membre.Login, result.Login);
        Assert.Equal(membre.Email, result.Email);
        Assert.Equal(membre.Nom, result.Nom);
        Assert.Equal(membre.Prenom, result.Prenom);
        Assert.Equal(membre.Telephone, result.Telephone);
        membreRepositoryMock.Verify(r => r.GetByIdAsync(new MembreId(query.Id)),
            Times.Once);
    }

    [Fact]
    public async Task GivenHandle_WhenInvalidQuery_ThenThrowException()
    {
        // Arrange
        var query = new GetMembreByIdQuery(Guid.Empty);
        var handler = MakeSut(out var membreRepositoryMock);

        // Act
        async Task act() => await handler.Handle(query, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<ValidationException>(act);
        membreRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<MembreId>()),
            Times.Never);
    }
}
