using Backend.DTOs;
using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CallController : ControllerBase
    {
        [HttpGet("CallsDto")]
        public IActionResult GetCalls()
        {
            try
            {
                using var cx = new CustomerserviceContext();
                var response = cx.Calls.Include(x => x.Customer).Include(y => y.Agent).Select(f=> new CallsDto {
                    CustomerName = f.Customer.CustomerName,
                    AgentName = f.Agent.AgentName,
                    PhoneNumber = f.PhoneNumber,
                    CallDate = f.CallDate,
                    CallTime = f.CallTime
                }).
                ToList();
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(400, "Hiba történt a hívások lekérdezése során.");
            }
        }
    }
}
