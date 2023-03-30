namespace PagedResult;

/// <summary>
/// Global paged result config
/// </summary>
public static class PaginationConfig
{
    /// <summary>
    /// Max items per page (default: 1000)
    /// </summary>
    public static int MaxPageSize { get; set; } = 1000;

    /// <summary>
    /// Min items per page (default: 20)
    /// </summary>
    public static int MinPageSize { get; set; } = 20;
}
