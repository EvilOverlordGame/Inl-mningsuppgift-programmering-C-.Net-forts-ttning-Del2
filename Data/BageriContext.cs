using bageri_api.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace bageri_api.Data;

public class BageriContext(DbContextOptions options) : IdentityDbContext<User>(options)
{
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<SalesOrder> SalesOrders { get; set; }
    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CartItem>().HasKey(c => new { c.CartId, c.ProductId });
        modelBuilder.Entity<OrderItem>().HasKey(c => new { c.ProductId, c.SalesOrderId });
        base.OnModelCreating(modelBuilder);
    }
}
