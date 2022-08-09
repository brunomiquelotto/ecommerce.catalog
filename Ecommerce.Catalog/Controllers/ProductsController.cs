using Ecommerce.Catalog.Data;
using Ecommerce.Catalog.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Catalog.Controllers {
    [Route("products")]
    public class ProductsController : ControllerBase {
        private readonly CatalogContext context;

        public ProductsController(CatalogContext context) {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> Index() {
            return await context.Products.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id) {
            var prod = await context.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);

            if (prod == null) {
                return NotFound();
            }

            return prod;
        }
        
        [HttpPost]
        public async Task<ActionResult<Product>> Post([FromBody]Product product) {
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
            return product;
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            var prod = await context.Products.FindAsync(id);

            if (prod == null) {
                return NotFound();
            }

            context.Products.Remove(prod);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
