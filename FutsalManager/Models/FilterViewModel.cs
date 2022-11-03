namespace FutsalManager.Models;

public class FilterViewModel
{
    public bool ShowDetailedFilter { get; set; }
    public bool ShowFilterTitles { get; set; } = true;
    public bool ShowSearchBar { get; set; } = true;
    public List<string> FilterTitles { get; set; } = new();
    public List<string> FilterOptions { get; set; } = new();
    public Type FilterModelType { get; set; }
}