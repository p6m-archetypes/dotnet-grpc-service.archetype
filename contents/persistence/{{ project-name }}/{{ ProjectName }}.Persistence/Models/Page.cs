namespace {{ ProjectName }}.Persistence.Models;

public class Page<T>
{
    public List<T> Items { get; set; }
    public long TotalElements { get; set; }
}