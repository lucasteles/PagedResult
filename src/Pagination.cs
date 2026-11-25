using System;
using Microsoft.AspNetCore.Mvc;

namespace PagedResult;

using static PaginationConfig;

/// <summary>
/// Provides pagination values bound from query parameters.
/// </summary>
public class Pagination
{
    /// <summary>
    /// Gets or sets the requested page number from the <c>page</c> query parameter.
    /// </summary>
    [FromQuery(Name = "page")]
    public int? PageQuery { get; set; }

    /// <summary>
    /// Gets or sets the requested page size from the <c>pageSize</c> query parameter.
    /// </summary>
    [FromQuery(Name = "pageSize")]
    public int? PageSizeQuery { get; set; }

    /// <summary>
    /// Gets the requested page size, clamped to the configured minimum and maximum.
    /// </summary>
    public int PageSize =>
        Math.Clamp(PageSizeQuery ?? DefaultPageSize, MinPageSize, MaxPageSize);

    /// <summary>
    /// Gets the requested page number, defaulting to the first page when the value is missing or less than one.
    /// </summary>
    public int Page =>
        PageQuery is null or < 1 ? 1 : PageQuery.Value;
}
