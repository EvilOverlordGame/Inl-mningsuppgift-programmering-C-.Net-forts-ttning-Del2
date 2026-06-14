namespace bageri_api.DTOs.Customers;

public class BaseCustomerDto
{
    public required string CompanyName { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Email { get; set; }
    public required string ContactPerson { get; set; }
}
