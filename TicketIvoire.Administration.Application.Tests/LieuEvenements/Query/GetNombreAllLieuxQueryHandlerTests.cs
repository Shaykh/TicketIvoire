using Microsoft.Extensions.Logging;
using Moq;
using TicketIvoire.Administration.Application.LieuEvenements.Query;
using TicketIvoire.Administration.Domain.LieuEvenements;

namespace TicketIvoire.Administration.Application.Tests.LieuEvenements.Query;

public class GetNombreAllLieuxQueryHandlerTests
{
    [Fact]
    public async Task GivenHandle_WhenCommand_ThenReturnCorrectValue()
    {
        // Arrange
        var lieuRepositoryMock = new Mock<ILieuRepository>();
        lieuRepositoryMock
            .Setup(r => r.GetCountAsync())
            .ReturnsAsync(100);
        var handler = new GetNombreAllLieuxQueryHandler(Mock.Of<ILogger<GetNombreAllLieuxQueryHandler>>(), lieuRepositoryMock.Object);

        // Act
        var result = await handler.Handle(new GetNombreAllLieuxQuery(), CancellationToken.None);

        // Assert
        Assert.Equal(100, result);
        lieuRepositoryMock.Verify(r => r.GetCountAsync(), 
            Times.Once);
    }
}
