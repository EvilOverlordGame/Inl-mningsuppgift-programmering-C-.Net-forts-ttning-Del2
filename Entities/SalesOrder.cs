namespace bageri_api.Entities;

public class SalesOrder
{
    public int SalesOrderId { get; set; }
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public Supplier Supplier { get; set; }
    public List<OrderItem> OrderItems { get; set; }
}
