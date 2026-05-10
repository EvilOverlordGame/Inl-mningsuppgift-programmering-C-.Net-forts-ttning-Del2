namespace bageri_api.DTOs.Products;

public class BaseProductDto
{
    public required string ProductName { get; set; }
    public double Price { get; set; }
    public int AmountInProduct { get; set; }
}
