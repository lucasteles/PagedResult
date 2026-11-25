using Microsoft.EntityFrameworkCore;
using PagedResult;

namespace System.Linq;

/// <summary>
/// Provides extension methods for creating paged collections from queryable and enumerable sources.
/// </summary>
public static class PagedExtensions
{
    /// <summary>
    /// Asynchronously creates a page from an entity framework query.
    /// </summary>
    /// <typeparam name="T">The type of the query items.</typeparam>
    /// <param name="query">The query to page.</param>
    /// <param name="pagination">The requested page and page size, or <see langword="null"/> to return all items.</param>
    /// <returns>A task containing the items and pagination metadata.</returns>
    public static async Task<Paged<T>> ToPagedAsync<T>(this IQueryable<T> query, Pagination? pagination = null)
    {
        if (pagination is null)
        {
            var result = await query.ToArrayAsync().ConfigureAwait(false);
            return new Paged<T>(result, result.Length, 1);
        }

        var totalCount = await query.CountAsync().ConfigureAwait(false);

        var pageSize = pagination.PageSize;
        var totalPages = GetTotalPages(totalCount, pageSize);
        var skip = GetSkip(pagination.Page, pageSize);
        var items = await query.Skip(skip).Take(pageSize).ToArrayAsync().ConfigureAwait(false);
        return new(items, totalCount, totalPages);
    }

    /// <summary>
    /// Creates a page from a collection already available in memory.
    /// </summary>
    /// <typeparam name="T">The type of the collection items.</typeparam>
    /// <param name="source">The collection to page.</param>
    /// <param name="pagination">The requested page and page size, or <see langword="null"/> to return all items.</param>
    /// <returns>The selected items and pagination metadata.</returns>
    public static Paged<T> ToPaged<T>(this IReadOnlyCollection<T> source, Pagination? pagination = null)
    {
        if (pagination is null)
            return new Paged<T>(source, source.Count, 1);

        var pageSize = pagination.PageSize;
        var totalPages = GetTotalPages(source.Count, pageSize);
        var skip = GetSkip(pagination.Page, pageSize);
        var items = source.Skip(skip).Take(pageSize).ToArray();
        return new(items, source.Count, totalPages);
    }

    /// <summary>
    /// Creates a single page containing all items in the sequence.
    /// </summary>
    /// <typeparam name="T">The type of the sequence items.</typeparam>
    /// <param name="items">The sequence to include.</param>
    /// <returns>A paged collection containing all items.</returns>
    public static Paged<T> ToPaged<T>(this IEnumerable<T> items) => new([.. items]);

    static int GetSkip(int page, int pageSize) => (int)Math.Min((long)(page - 1) * pageSize, int.MaxValue);

    static int GetTotalPages(int total, int pageSize) =>
        (int)Math.Ceiling((double)total / Math.Max(pageSize, 1));
}
