using bageri_api.Entities;
using bageri_api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace bageri_api.Controllers;

[Route("api/customers")]
[ApiController]
public class CustomersController(IGenericRepository<Customer> repo) : ControllerBase
{
    [HttpGet()]
    public async Task<ActionResult> ListAllCustomers()
    {
        try
        {
            var customers = await repo.ListAllAsync();
            return Ok(new { Success = true, StatusCode = 200, Items = customers.Count, Data = customers });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Sorry, något gick fel {ex.Message}");
        }
    }
    [HttpGet("{id}")]
    public async Task<ActionResult> FindCustomer(int id)
    {
        try
        {
            var customer = await repo.FindByIdAsync(id);
            return Ok(new { Success = true, StatusCode = 200, Items = 1, Data = customer });
        }
        catch
        {
            return NotFound("Hittade inget");
        }
    }

    [HttpPost()]
    public async Task<ActionResult> AddCustomer(Customer customer)
    {
        try
        {
            repo.Add(customer);
            if (await repo.SaveAllAsync()) return StatusCode(201);

            return StatusCode(500, "Något gick fell med leverantör");
        }
        catch
        {
            return StatusCode(500, "Något gick fell med leverantör");
        }
    }
}

