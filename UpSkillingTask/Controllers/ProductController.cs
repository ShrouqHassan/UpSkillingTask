using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;
using UpSkillingTask.Models;

namespace UpSkillingTask.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext Context;
        public ProductController(ApplicationDbContext _Context)
        {
            Context = _Context;  
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Context.Products);
        }
        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            if (ModelState.IsValid) { 
                Context.Products.Add(product);
                Context.SaveChanges();
                return Created();
            }
            throw new Exception("Invalid Object");
        }
        [HttpPut]
        public IActionResult Put(int productId,[FromBody] Product product) {
            var ExistingProduct = Context.Products.AsNoTracking().FirstOrDefault(p => p.ProductId == productId);
            if (ExistingProduct==null) {
                return NotFound("Product Not Found");
            }
            if (ModelState.IsValid) {
                product.ProductId = productId;
               Context.Entry(product).State = EntityState.Modified;
                Context.SaveChanges();  
                return Ok(product); 
            }
            return BadRequest("Invalid Object");
        }
        [HttpDelete]
        public IActionResult Delete(int productId) { 
            var ExistingProduct = Context.Products.FirstOrDefault(p=>p.ProductId == productId);
            if (ExistingProduct == null) {
                return NotFound();
            }
            Context.Products.Remove(ExistingProduct);
            Context.SaveChanges();  
            return Ok();
        }
    }
}
