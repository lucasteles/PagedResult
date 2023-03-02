using System;

namespace PagedResult;

using Microsoft.AspNetCore.Mvc;

public class Pagination
{
    public static int MaxPageSize { get; set; } = 1000;
    public static int MinPageSize { get; set; } = 20;

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
