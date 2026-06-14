using bageri_api.Data;
using bageri_api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace bageri_api.Controllers;

[Route("api/orders")]
[ApiController]
public class OrdersController(BageriContext context) : ControllerBase
{
    public async Task<ActionResult> Pay([FromQuery] int customerId)
    {
        Customer customer = await context.Customers.FindAsync(customerId);
        if (customer is null) return BadRequest("Kund finns inte!!!");

        Cart cart = await context.Carts
            .Include(c => c.CartItems)
            .SingleOrDefaultAsync(c => c.CustomerId == customerId);
        if (cart is null) return BadRequest("Finns ingen kundvagn för kunden...");

        List<OrderItem> items = [.. cart.CartItems.Select(o => new OrderItem{
            ProductId = o.ProductId,
            Quantity = o.Quantity,
            Price = o.Price
        })];

        SalesOrder order = new()
        {
            Customer = customer,
            OrderItems = items
        };

        context.SalesOrders.Add(order);

        context.Carts.Remove(cart);

        await context.SaveChangesAsync();
        return StatusCode(201);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetOrderById(int id)
    {
        SalesOrder salesOrder = await context.SalesOrders.Include(c => c.Customer)
        .SingleOrDefaultAsync(c => c.SalesOrderId == id);

        var data = new
        {
            salesOrder.CustomerId,
            salesOrder.Customer,
            salesOrder.Customer.CompanyName,
            salesOrder.Customer.ContactPerson,
            salesOrder.Customer.Email,
            salesOrder.OrderDate
        };

        return Ok(new { Success = true, StatusCode = 200, Items = "Not defined", Data = data });
    }
}
