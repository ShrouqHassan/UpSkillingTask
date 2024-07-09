using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UpSkillingTask.Models;

namespace UpSkillingTask.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext Context;

        public OrderController(ApplicationDbContext _Context)
        {
            Context = _Context;

        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Context.Orders.Include(o=>o.Products).ToList());
        }
        [HttpPost]
        public IActionResult Post([FromBody] Order order)
        {
            if (ModelState.IsValid)
            {
                Context.Orders.Add(order);
                Context.SaveChanges();
                return Created();
            }
            throw new Exception("Invalid Object");
        }
        [HttpPut]
        public IActionResult Put(int orderId, [FromBody] Order order)
        {
            var Existingorder = Context.Orders.AsNoTracking().FirstOrDefault(o => o.OrderId == orderId);
            if (Existingorder == null)
            {
                return NotFound("order Not Found");
            }
            if (ModelState.IsValid)
            {
                order.OrderId = orderId;
                Context.Entry(order).State = EntityState.Modified;
                Context.SaveChanges();
                return Ok(order);
            }
            return BadRequest("Invalid Object");
        }
        [HttpDelete]
        public IActionResult Delete(int orderId)
        {
            var Existingorder = Context.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (Existingorder == null)
            {
                return NotFound();
            }
            Context.Orders.Remove(Existingorder);
            Context.SaveChanges();
            return Ok();
        }
    }
}
