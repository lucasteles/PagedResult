using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PagedResult;

namespace System.Linq;

/// <summary>
/// PagedResult extensions for IEnumerable
/// </summary>
public static class EnumerableExtensions
{
    public static async Task<Paged<T>> ToPagedAsync<T>(
        this IQueryable<T> query,
        Pagination? pagination = null)
    {
        if (pagination is null)
        {
            var result = await query.ToArrayAsync();
            return new Paged<T>(result, result.Length, 1);
        }

        var totalCount = await query.CountAsync();
        var size = pagination.PageSize <= 0 ? 1 : pagination.PageSize;
        var totalPages = (int)Math.Ceiling((double)totalCount / size);

        var items = await query
            .Skip(pagination.Skip)
            .Take(pagination.PageSize)
            .ToArrayAsync();

        return new Paged<T>(items, totalCount, totalPages);
    }

    public static Paged<T> ToPaged<T>(
        this IReadOnlyCollection<T> source,
        Pagination? pagination = null)
    {
        if (pagination is null)
            return new Paged<T>(source, source.Count, 1);

        var size = pagination.PageSize <= 0 ? 1 : pagination.PageSize;
        var totalPages = (int)Math.Ceiling((double)source.Count / size);

        var items = source
            .Skip(pagination.Skip)
            .Take(pagination.PageSize);

        return new Paged<T>(items, source.Count, totalPages);
    }

    public static Paged<T> ToPaged<T>(this IEnumerable<T> items) => new(items);
}
