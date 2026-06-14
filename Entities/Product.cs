using System.ComponentModel.DataAnnotations.Schema;

namespace bageri_api.Entities;

public class Product : BaseEntity
{
    public int CustomerId { get; set; }
    public required string ProductName { get; set; }
    public double Price { get; set; }
    public double Weight { get; set; }
    public int AmountInProduct { get; set; }
    public DateTime TimeMade { get; set; } = DateTime.Now;
    [ForeignKey("CustomerId")]
    public Customer Customer { get; set; }
}
