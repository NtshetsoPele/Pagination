namespace Shared.RequestFeatures;

// Holds the common properties for all the
// entities in our project.
public class RequestParameters
{
    // Restrict our API to a maximum of 50 rows per page
    private const byte MaxPageSize = 50;
    private byte _pageSize = 2;
    
    public int PageNumber { get; init; } = 1;
    public byte PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
}