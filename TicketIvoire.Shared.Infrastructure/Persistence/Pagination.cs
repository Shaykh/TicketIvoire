namespace TicketIvoire.Shared.Infrastructure.Persistence;

public static class Pagination
{
    private const int DefaultPageSize = 20;

    public static IQueryable<T> ToPaging<T>(this IQueryable<T> enumerable, uint? pageNumber, uint? numberByPage)
    {
        if (!IsPaginationRequested(pageNumber, numberByPage))
        {
            return enumerable;
        }
        (int numberToTake, int numberToSkip) = CheckPaginationValidity(numberByPage!.Value, pageNumber!.Value) ?
            ((int)numberByPage!.Value, (int)pageNumber!.Value) : (DefaultPageSize, 1);
        return enumerable
                .Skip((numberToSkip - 1) * numberToTake)
                .Take(numberToTake);
    }

    private static bool IsPaginationRequested(uint? pageNumber, uint? numberByPage)
        => pageNumber.HasValue && numberByPage.HasValue;

    private static bool CheckPaginationValidity(uint pageSize, uint pageNumber)
        => pageSize > 0 && pageNumber > 0;
}
