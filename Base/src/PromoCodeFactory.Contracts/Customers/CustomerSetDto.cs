namespace PromoCodeFactory.Contracts.Customers;

public record CustomerSetDto
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Email { get; set; }
}