namespace PropertyListing.Application.Properties.Queries.Dtos;

public class ComparisonDto
{
    public List<PropertyComparisonDto> Properties { get; set; } = new();
    public Dictionary<string, List<string>> Differences { get; set; } = new();
    public Dictionary<string, List<string>> Similarities { get; set; } = new();
}