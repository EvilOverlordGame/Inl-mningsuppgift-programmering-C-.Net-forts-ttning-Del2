namespace bageri_api.DTOs.Customers;

public class GetCustomerDto : GetCustomersDto
{
    public string AddressLine { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
}
