using System;
using Microsoft.AspNetCore.Mvc;

namespace PagedResult;

using static PaginationConfig;

/// <summary>
/// Pagination route to be used with [AsParameters]
/// </summary>
public class Pagination
{
    [FromQuery(Name = "page")]
    public int? PageQuery { get; set; }

    [FromQuery(Name = "pageSize")]
    public int? PageSizeQuery { get; set; }

    public int PageSize =>
        Math.Clamp(PageSizeQuery ?? MinPageSize, MinPageSize, MaxPageSize);

    public int Page =>
        PageQuery is null or < 1 ? 1 : PageQuery.Value;

    public int Skip => (Page - 1) * PageSize;
}
