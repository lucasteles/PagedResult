using System.Collections.Generic;
using System.Linq;

namespace PagedResult;

/// <summary>
/// Represents a collection of items and its pagination metadata.
/// </summary>
/// <typeparam name="TValue">The type of the items in the collection.</typeparam>
public sealed class Paged<TValue>(IReadOnlyCollection<TValue> data, int totalItems, int totalPages)
{
    /// <summary>
    /// Initializes a paged collection with the supplied items, using their count as the total item count
    /// and setting the total page count to one.
    /// </summary>
    /// <param name="data">The items in this page.</param>
    public Paged(IReadOnlyCollection<TValue> data) : this(data, data.Count, 1) { }

    /// <summary>
    /// Gets the items in this page.
    /// </summary>
    public IReadOnlyCollection<TValue> Data { get; init; } = data;

    /// <summary>
    /// Gets the total number of items across all pages.
    /// </summary>
    public int TotalItems { get; init; } = totalItems;

    /// <summary>
    /// Gets the total number of pages.
    /// </summary>
    public int TotalPages { get; init; } = totalPages;

    /// <summary>
    /// Converts an array of items to a paged collection.
    /// </summary>
    /// <param name="items">The items to include.</param>
    /// <returns>A paged collection containing the items.</returns>
    public static implicit operator Paged<TValue>(TValue[] items) => new(items);

    /// <summary>
    /// Converts a list of items to a paged collection.
    /// </summary>
    /// <param name="items">The items to include.</param>
    /// <returns>A paged collection containing the items.</returns>
    public static implicit operator Paged<TValue>(List<TValue> items) => new(items);
}
