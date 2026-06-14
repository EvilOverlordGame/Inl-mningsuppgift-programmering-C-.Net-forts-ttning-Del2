using System.Text.Json;
using bageri_api.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace bageri_api.Data;

public class SeedDataBase(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
{
    private static readonly JsonSerializerOptions options = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public async Task SeedSuppliers(BageriContext context)
    {
        if (context.Customers.Any()) return;

        var json = File.ReadAllText("Data/Json/suppliers.json");
        Console.WriteLine(json);
        var customers = JsonSerializer.Deserialize<List<Customer>>(json, options);

        if (customers is not null && customers.Count > 0)
        {
            await context.Customers.AddRangeAsync(customers);
            await context.SaveChangesAsync();
        }
    }

    public async Task SeedProducts(BageriContext context)
    {
        if (context.Products.Any()) return;

        var json = File.ReadAllText("Data/Json/products.json");
        var products = JsonSerializer.Deserialize<List<Product>>(json, options);

        if (products is not null && products.Count > 0)
        {
            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }
    }
}
