namespace Nikander.Umbraco.VariantCopy.Models;
public class PublishedCultureResponse
{
    public bool IsPublished { get; set; }
    public string? Name { get; set; } = string.Empty;
    public string? IsoCode { get; set; } = string.Empty;
    public int Id { get; set; }
}
