namespace bageri_api.Entities;

public class OrderItem
{
    public int SalesOrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public Product Product { get; set; }
    public SalesOrder SalesOrder { get; set; }
}
