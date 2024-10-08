using System.ComponentModel.DataAnnotations;

namespace PromoCodeFactory.Contracts.Roles;

public record RoleSetDto
{
    [StringLength(50, MinimumLength = 2)]
    public required string Name { get; set; }
    
    [StringLength(100, MinimumLength = 2)]
    public required string Description { get; set; }
    
}