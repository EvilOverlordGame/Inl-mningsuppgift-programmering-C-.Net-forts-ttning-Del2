namespace bageri_api.DTOs.Customers;

public class PostCustomerDto
{
    public required string CompanyName { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Email { get; set; }
    public required string ContactPerson { get; set; }
    public required string AddressLine { get; set; }
    public required string PostalCode { get; set; }
    public required string City { get; set; }
}
