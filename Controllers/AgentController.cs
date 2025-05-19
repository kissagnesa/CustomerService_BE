using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                using (var cx = new CustomerserviceContext())
                {
                    var agents = cx.Agents.ToList();
                    return Ok(agents);
                }            
            }
            catch (Exception)
            { 
                return StatusCode(400);
            }
        }

        [HttpGet("{ById}")]
        public IActionResult GetById(int id)
        {
            try
            {
                using (var cx = new CustomerserviceContext())
                {
                    var agent = cx.Agents.FirstOrDefault(x => x.AgentId == id);
                    if (agent == null)
                        return NotFound("Nincs ilyen azonosítójú kolléga!");
                    return Ok(agent);
                }
            }
            catch (Exception)
            {
                return StatusCode(400);
            }
        }

        [HttpPost("NewAgent")]
        public IActionResult PostAgent(string name)
        {
            try
            {
                using (var cx = new CustomerserviceContext())
                {
                    var agent = new Agent();
                    agent.AgentName = name;
                    cx.Agents.Add(agent);
                    cx.SaveChanges();
                    return Ok("Új kolléga hozzáadása sikeres!");
                }
            }
            catch (Exception)
            {
                return StatusCode(401, "Nincs jogosultsága új kolléga felvételéhez!");
            }
        }

        [HttpPut("UpdateAgent")]
        public IActionResult PutAgent(int id, string name)
        {
            try
            {
                using (var cx = new CustomerserviceContext())
                {
                    var agent = cx.Agents.FirstOrDefault(x => x.AgentId == id);
                    if (agent == null)
                        return NotFound("Nincs ilyen azonosítójú kolléga!");
                    agent.AgentName = name;
                    cx.SaveChanges();
                    return Ok("Kolléga módosítása sikeres!");
                }
            }
            catch (Exception)
            {
                return StatusCode(401, "Nincs jogosultsága kolléga módosításához!");
            }
        }

        [HttpDelete("DeleteAgent")]
        public IActionResult DeleteAgent(int id)
        {
            try
            {
                using (var cx = new CustomerserviceContext())
                {
                    var agent = cx.Agents.FirstOrDefault(x => x.AgentId == id);
                    if (agent == null)
                        return NotFound("Nincs ilyen azonosítójú kolléga!");
                    cx.Agents.Remove(agent);
                    cx.SaveChanges();
                    return Ok("Kolléga törlése sikeres!");
                }
            }
            catch (Exception)
            {
                return StatusCode(401, "Nincs jogosultsága kolléga törléséhez!");
            }
        }
    }
}
