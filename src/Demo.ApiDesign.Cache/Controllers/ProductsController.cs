using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.ApiDesign.Cache.Context;
using Demo.ApiDesign.Cache.Model;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.ApiDesign.Cache.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    { 
        private DemoDbContext _context;
        public ProductsController(DemoDbContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        [HttpGet("{id}", Name = "Products_GetProduct")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _context.Products.Where(x => x.ProductId == id).FirstOrDefaultAsync();
            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
           return Ok(await _context.Products.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return CreatedAtRoute("Products_GetProduct", new { id = product.ProductId }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Product product)
        {
            if (await _context.Products.FindAsync(id) == null)
            {
                return NoContent();
            }

            // Update entity in DbSet
            _context.Products.Update(product);

            // Save changes in database
            await _context.SaveChangesAsync();
            return Ok(product);
        }
    }
}