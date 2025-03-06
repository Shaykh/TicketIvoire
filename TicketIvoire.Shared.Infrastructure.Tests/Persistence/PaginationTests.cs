using TicketIvoire.Shared.Infrastructure.Persistence;

namespace TicketIvoire.Shared.Infrastructure.Tests.Persistence;

#pragma warning disable CA1707 
public class PaginationTests
{
    private const int DefaultPageSize = 20;

    [Fact]
    public void GivenToPaging_WhenNumberByPageIsNull_ThenReturnAllItems()
    {
        // Arrange
        IQueryable<int> items = Enumerable.Range(1, 100).AsQueryable();
        uint? numberByPage = null;
        uint? pageNumber = 10;

        // Act
        IQueryable<int> result = items.ToPaging(pageNumber, numberByPage);

        // Assert
        Assert.Equal(100, result.Count());
    }

    [Fact]
    public void GivenToPaging_WhenPageNumberIsNull_ThenReturnAllItems()
    {
        // Arrange
        IQueryable<int> items = Enumerable.Range(1, 100).AsQueryable();
        uint? numberByPage = 10;
        uint? pageNumber = null;

        // Act
        IQueryable<int> result = items.ToPaging(pageNumber, numberByPage);

        // Assert
        Assert.Equal(items.Count(), result.Count());
    }

    [Fact]
    public void GivenToPaging_WhenPageNumberAndNumberByPageAreNull_ThenReturnAllItems()
    {
        // Arrange
        IQueryable<int> items = Enumerable.Range(1, 100).AsQueryable();
        uint? numberByPage = null;
        uint? pageNumber = null;

        // Act
        IQueryable<int> result = items.ToPaging(pageNumber, numberByPage);

        // Assert
        Assert.Equal(items.Count(), result.Count());
    }

    [Fact]
    public void GivenToPaging_WhenPageNumberIsZero_ThenReturnDefaultNumberOfItemsOfPageOne()
    {
        // Arrange
        IQueryable<int> items = Enumerable.Range(1, 100).AsQueryable();
        uint? numberByPage = 10;
        uint? pageNumber = 0;

        // Act
        IQueryable<int> result = items.ToPaging(pageNumber, numberByPage);

        // Assert
        Assert.Equal(DefaultPageSize, result.Count());
    }

    [Fact]
    public void GivenToPaging_WhenNumberByPageIsZero_ThenReturnDefaultNumberOfItemsOfPageOne()
    {
        // Arrange
        IQueryable<int> items = Enumerable.Range(1, 100).AsQueryable();
        uint? numberByPage = 0;
        uint? pageNumber = 10;

        // Act
        IQueryable<int> result = items.ToPaging(pageNumber, numberByPage);

        // Assert
        Assert.Equal(DefaultPageSize, result.Count());
    }

    [Fact]
    public void GivenToPaging_WhenNumberByPageIsZeroAndPageNumberIsZero_ThenReturnDefaultNumberOfItemsOfPageOne()
    {
        // Arrange
        IQueryable<int> items = Enumerable.Range(1, 100).AsQueryable();
        uint? numberByPage = 0;
        uint? pageNumber = 0;

        // Act
        IQueryable<int> result = items.ToPaging(pageNumber, numberByPage);

        // Assert
        Assert.Equal(DefaultPageSize, result.Count());
    }

    [Fact]
    public void GivenToPaging_WhenNumberByPageIsMoreThanZeroAndPageNumberIsMoreThanZero_ThenReturnCorrectItems()
    {
        // Arrange
        IQueryable<int> items = Enumerable.Range(1, 100).AsQueryable();
        uint? numberByPage = 10;
        uint? pageNumber = 4;

        // Act
        IQueryable<int> result = items.ToPaging(pageNumber, numberByPage);

        // Assert
        Assert.Equal((int)numberByPage, result.Count());
        Assert.Equal(31, result.First());
        Assert.Equal(40, result.Last());
    }
}
#pragma warning restore CA1707 
