namespace bageri_api.DTOs.Products;

public class GetProductDto : GetProductsDto
{
    public double Weight { get; set; }

    public DateTime TimeMade { get; set; } = DateTime.Now;
}
