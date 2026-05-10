namespace bageri_api.DTOs.Suppliers;

public class GetSupplierDto : GetSuppliersDto
{
    public string AddressLine { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
}
