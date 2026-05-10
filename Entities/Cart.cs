using System.ComponentModel.DataAnnotations.Schema;

namespace bageri_api.Entities;

public class Cart
{
    public int CartId { get; set; }
    public int SupplierId { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    [ForeignKey("SupplierId")]
    public Supplier Supplier { get; set; }
    public List<CartItem> CartItems { get; set; }
}
