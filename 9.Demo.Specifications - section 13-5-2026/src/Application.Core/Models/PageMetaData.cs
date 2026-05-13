namespace Application.Core;

public class PageMetaData<T>
{
    const int MaxPageSize = 50;
    private int _currentPageSize = 10;
    private string? _search;

    public List<T>? Data { get; set; }
    public int TotalItemsInDb { get; set; }

    public int CurrentPageIndex { get; set; } = 1;

    public int CurrentPageSize
    {
        get => _currentPageSize;
        set => _currentPageSize = value > MaxPageSize ? MaxPageSize : value;
    }

    public string? Search
    {
        get => _search;
        set => _search = value?.ToUpperInvariant();
    }

    public string? SortBy { get; set; }

    public int TotalPages => (int)Math.Ceiling((double)TotalItemsInDb / CurrentPageSize);  // عدد الصفحات
    public bool HasNext => CurrentPageIndex < TotalPages;
    public bool HasPrevious => CurrentPageIndex > 1;
}