namespace {{ ProjectName }}.Persistence.Models;

public class PageRequest
{
    public int StartPage { get; set; }
    public int PageSize { get; set; }
}