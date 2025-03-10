using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using TicketIvoire.Administration.Application.LieuEvenements.Query;
using TicketIvoire.Administration.Domain.LieuEvenements;

namespace TicketIvoire.Administration.Application.Tests.LieuEvenements.Query;

public class GetNombreAllLieuxByVilleQueryHandlerTests
{
    private static GetNombreAllLieuxByVilleQueryHandler MakeSut(out Mock<ILieuRepository> lieuRepositoryMock)
    {
        var validator = new GetNombreAllLieuxByVilleQueryValidator();
        lieuRepositoryMock = new();

        return new GetNombreAllLieuxByVilleQueryHandler(Mock.Of<ILogger<GetNombreAllLieuxByVilleQueryHandler>>(), validator, lieuRepositoryMock.Object);
    }

    [Fact]
    public async Task GivenHandle_WhenInvalidQuery_ThenThrowException()
    {
        // Arrange
        var query = new GetNombreAllLieuxByVilleQuery("");
        var handler = MakeSut(out var lieuRepositoryMock);

        // Act
        async Task act() => await handler.Handle(query, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<ValidationException>(act);
        lieuRepositoryMock.Verify(r => r.GetCountByVilleAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task GivenHandle_WhenValidCommand_ThenReturnCorrectValue()
    {
        // Arrange
        var query = new GetNombreAllLieuxByVilleQuery("ville");
        var handler = MakeSut(out var lieuRepositoryMock);
        lieuRepositoryMock
            .Setup(r => r.GetCountByVilleAsync(query.Ville, CancellationToken.None))
            .ReturnsAsync(10);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(10, result);
        lieuRepositoryMock.Verify(r => r.GetCountByVilleAsync(query.Ville, CancellationToken.None),
            Times.Once);
    }
}
