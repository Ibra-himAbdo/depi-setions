namespace Application.Core;

public record BaseSpecificationParams
{
    public bool WithIncludes { get; set; } = true;

    private const int MaxPageSize = 20;
    private int _pageSize = 10;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
    }

    public int PageIndex { get; set; } = 1;
    public string? Sort { get; set; }

    private string? _search;

    public string? Search
    {
        get => _search;
        set => _search = value?.ToUpperInvariant();
    }
}