using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private int id;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAll()
        {
            try
            {
                using (var cx = new CustomerserviceContext())
                {
                    var customers = await cx.Customers.ToListAsync();
                    return Ok(customers);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Hiba történt az ügyfelek lekérdezése során.");
            }
        }

        [HttpGet("{ById}")]
        public async Task<ActionResult<Customer>> GetById(int id)
        {
            try
            {
                using (var cx = new CustomerserviceContext())
                {
                    var customer = await cx.Customers.FindAsync(id);
                    if (customer == null)
                        return NotFound("Nincs ilyen azonosítójú ügyfél!");
                    return Ok(customer);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Hiba történt az ügyfél lekérdezése során.");
            }
        }

        [HttpPost("NewCustomer")]
        public async Task<ActionResult<string>> PostCustomer(string name)
        {
            try
            {
                using (var cx = new CustomerserviceContext())
                {
                    var customer = new Customer();
                    customer.CustomerName = name;
                    cx.Customers.Add(customer);
                    await cx.SaveChangesAsync();
                    return Ok("Új ügyfél hozzáadása sikeres!");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Hiba történt az ügyfél hozzáadása során.");
            }
        }

        [HttpPut("UpdateCustomer")]
        public async Task<ActionResult<string>> PutCustomer(Customer customer)
        {
            try
            {
                using (var cx = new CustomerserviceContext())
                {
                    var response=cx.Customers.FirstOrDefault(x => x.CustomerId == customer.CustomerId);
                    if (response!=null)
                    { 
                        cx.Customers.Update(customer);
                        await cx.SaveChangesAsync();
                        return Ok("Ügyfél módosítása sikeresen megtörtént!");
                    }
                    else
                    {
                        return NotFound("Nincs ilyen azonosítójú ügyfél!");
                    }
                }
            }
            catch (Exception)
            {
                return StatusCode(404, "Hiba történt az ügyfél módosítása során.");
            }
        }

        [HttpDelete("DeleteCustomer")]
        public async Task<ActionResult<string>> DeleteCustomer(int id)
        {
            try
            {
                using (var cx = new CustomerserviceContext())
                {
                    var response = cx.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.CustomerId == id);
                    if(response != null)
                    {
                        cx.Customers.Remove(new Customer { CustomerId=id});
                        await cx.SaveChangesAsync();
                        return Ok("Ügyfél törlése sikeresen megtörtént!");
                    }
                    else
                    {
                        return StatusCode(404);
                    }
                }
            }
            catch (Exception)
            {
                return StatusCode(404, "Nincs ilyen azonosítójú ügyfél!");
            }
        }
    }
}
