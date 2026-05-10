using bageri_api.Data;
using bageri_api.DTOs.Carts;
using bageri_api.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bageri_api.Controllers;

[Route("api/carts")]
[ApiController]
public class CartsController(BageriContext context) : ControllerBase
{
    [HttpPost()]
    public async Task<ActionResult> AddCartItem(PostCartsItemsDto model)
    {
        try
        {
            Supplier supplier = await context.Suppliers.FindAsync(model.SupplierId);
            if (supplier is null) return BadRequest("Kunden existerar inte!");

            Product product = await context.Products.FindAsync(model.ProductId);
            if (product is null) return BadRequest("Produkten existerar inte!");

            Cart cart = await context.Carts.Include(c => c.CartItems).SingleOrDefaultAsync(c => c.SupplierId == model.SupplierId);

            if (cart is null)
            {
                cart = new Cart
                {
                    Supplier = supplier
                };

                context.Carts.Add(cart);
            }

            if (cart.CartItems is not null)
            {
                var foundItem = cart.CartItems.SingleOrDefault(i => i.ProductId == model.ProductId);
                if (foundItem is not null)
                {
                    foundItem.Quantity = model.Quantity;
                }
                else
                {
                    CartItem cartItem = new()
                    {
                        Cart = cart,
                        Product = product,
                        Quantity = model.Quantity,
                        Price = model.Price
                    };

                    context.CartItems.Add(cartItem);
                }
            }
            else
            {
                CartItem cartItem = new()
                {
                    Cart = cart,
                    Product = product,
                    Quantity = model.Quantity,
                    Price = model.Price
                };

                context.CartItems.Add(cartItem);
            }

            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(FindCart),
                new { id = cart.CartId },
                new { cart.CartId, cart.CreatedDate });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, "Något gick fel när kundvagnen skulle sparas...");
        }
    }

    [HttpGet()]
    public async Task<ActionResult> ListAllCarts()
    {
        var carts = await context.Carts
        .Select(cart => new
        {
            cart.CartId,
            cart.Supplier,
            cart.Supplier.CompanyName,
            cart.Supplier.ContactPerson,
            cart.Supplier.Email,
            CreatedDate = cart.CreatedDate.ToShortDateString()
        })
        .ToListAsync();

        return Ok(new { Success = true, StatusCode = 200, Items = carts.Count, Data = carts });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> FindCart(int id)
    {
        Cart cart = await context.Carts
            .Include(c => c.Supplier)
            .SingleOrDefaultAsync(c => c.CartId == id);

        var data = new
        {
            cart.CartId,
            cart.Supplier,
            cart.Supplier.CompanyName,
            cart.Supplier.ContactPerson,
            cart.Supplier.Email,
            CreatedDate = cart.CreatedDate.ToShortDateString()
        };

        return Ok(new { Success = true, StatusCode = 200, Items = "Not defined", Data = data });
    }
    /*
    [HttpGet("orderMade/{createdDate}")]
    public async Task<ActionResult> FindCartByDate(DateTime createdDate)
    {
        Cart cart = await context.Carts
            .SingleOrDefaultAsync(c => c.CreatedDate == createdDate);

        var data = new
        {
            cart.CartId,
            cart.Supplier,
            cart.Supplier.CompanyName,
            cart.Supplier.ContactPerson,
            cart.Supplier.Email,
            CreatedDate = cart.CreatedDate.ToShortDateString()
        };

        return Ok(new { Success = true, StatusCode = 200, Items = "Not defined", Data = data });
    } */

    [HttpGet("orderMade/{createdDate}")]
    public async Task<ActionResult> FindCartByDate([FromRoute] DateTime createdDate)
    {
        var start = createdDate.Date;
        var end = start.AddDays(1);

        var cart = await context.Carts
            .SingleOrDefaultAsync(c => c.CreatedDate >= start && c.CreatedDate < end);

        if (cart == null)
        {
            return NotFound(new { Success = false, Message = "Cart not found" });
        }

        var data = new
        {
            cart.CartId,
            cart.Supplier,
            cart.Supplier.CompanyName,
            cart.Supplier.ContactPerson,
            cart.Supplier.Email,
            CreatedDate = cart.CreatedDate.ToShortDateString()
        };

        return Ok(new
        {
            Success = true,
            StatusCode = 200,
            Items = "Not defined",
            Data = data
        });
    }
}


