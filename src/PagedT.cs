using System.Collections.Generic;
using System.Linq;

namespace PagedResult;

/// <summary>
/// Paged return value type
/// </summary>
/// <typeparam name="TValue"></typeparam>
public sealed class Paged<TValue>
{
    public IEnumerable<TValue> Data { get; init; }
    public int TotalItems { get; init; }
    public int TotalPages { get; init; }

    public Paged(IEnumerable<TValue> data, int totalItems, int totalPages)
    {
        Data = data;
        TotalItems = totalItems;
        TotalPages = totalPages;
    }

    public Paged(IEnumerable<TValue> data)
    {
        var items = data.ToArray();
        Data = items;
        TotalPages = 1;
        TotalItems = items.Length;
    }

    public Paged(params TValue[] items) : this(items.AsEnumerable()) { }
    public static implicit operator Paged<TValue>(TValue[] items) => new(items);
    public static implicit operator Paged<TValue>(List<TValue> items) => new(items);
}
