namespace Pcf.Dictionary.WebHost.Models;

public record PreferenceResponse
{
    public required Guid Id { get; set; }
        
    public required string Name { get; set; }
}