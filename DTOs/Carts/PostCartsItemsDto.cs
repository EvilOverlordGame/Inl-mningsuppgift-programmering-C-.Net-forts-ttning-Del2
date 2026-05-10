namespace bageri_api.DTOs.Carts;

public class PostCartsItemsDto
{
    public int SupplierId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
