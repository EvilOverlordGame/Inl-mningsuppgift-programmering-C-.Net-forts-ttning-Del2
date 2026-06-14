using System.ComponentModel.DataAnnotations.Schema;

namespace bageri_api.Entities;

public class Cart
{
    public int CartId { get; set; }
    public int CustomerId { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    [ForeignKey("CustomerId")]
    public Customer Customer { get; set; }
    public List<CartItem> CartItems { get; set; }
}
