namespace bageri_api.DTOs.Products;

public class PostProductDto
{
    public int CustomerId { get; set; }
    public required string ProductName { get; set; }
    public double Price { get; set; }
    public double Weight { get; set; }
    public int AmountInProduct { get; set; }
    //public DateTime TimeMade { get; set; } = DateTime.Now;
}
