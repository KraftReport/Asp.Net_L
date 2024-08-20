using CookieBasedAuthDemo.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CookieBasedAuthDemo.Product
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context) 
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Policy = "UserAndAdmin")]
        public async Task<ActionResult> getAllProducts()
        {
            return Ok(await _context.Products.ToListAsync());
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "UserAndAdmin")]
        public async Task<ActionResult> getProductById(int id)
        {
            return Ok(await _context.Products.FindAsync(id));
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult> createProduct([FromBody] Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return Ok(product);
        }

        [HttpPut]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult> updateProduct([FromBody] Product product)
        {
            var found = await _context.Products.FindAsync(product.Id);
            found.Name = product.Name;
            found.Price = product.Price;
            await _context.SaveChangesAsync();
            return Ok(found);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy ="AdminOnly")]
        public async Task<ActionResult> deleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
              _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return Ok(id);
        }

    }
}
