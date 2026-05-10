using AutoMapper;
using bageri_api.DTOs.Products;
using bageri_api.Entities;
using bageri_api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace bageri_api.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController(IGenericRepository<Product> repo, IMapper mapper) : ControllerBase
{
    [HttpGet()]
    public async Task<ActionResult> ListAllProducts()
    {
        try
        {
            var products = await repo.ListAllAsync();
            var productsDto = mapper.Map<IList<GetProductDto>>(products);
            return Ok(new { Success = true, StatusCode = 200, Items = products.Count, Data = productsDto });
        }
        catch
        {
            return StatusCode(500, "Något server fel inträffade");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> FindProduct(int id)
    {
        try
        {
            var product = await repo.FindByIdAsync(id);
            return Ok(new { Success = true, StatusCode = 200, Items = 1, Data = product });
        }
        catch
        {
            return StatusCode(500, "Något server fel inträffade, vi kan tyvärr inte hitta produkten.");
        }
    }

    [HttpPost()]
    public async Task<ActionResult> AddProduct(PostProductDto model)
    {
        try
        {
            var product = mapper.Map<Product>(model);

            repo.Add(product);

            if (await repo.SaveAllAsync())
            {
                return StatusCode(201);
            }

            return StatusCode(500, "Något server fel inträffade");
        }
        catch
        {
            return StatusCode(500, "Något server fel inträffade");
        }

    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> PatchProduct(int id, Product product)
    {
        try
        {
            var productToPatch = await repo.FindAsync(c => c.Id == id);
            if (productToPatch is null) return NotFound();

            productToPatch.ProductName = product.ProductName;
            productToPatch.Price = product.Price;

            if (await repo.SaveAllAsync()) return NoContent();
            return StatusCode(500, "Ett server fel inträffade");
        }
        catch
        {
            return StatusCode(500, "Ett server fel inträffade");

        }
    }
}

