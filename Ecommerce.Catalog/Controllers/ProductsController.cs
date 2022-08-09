using Ecommerce.Catalog.Data;
using Ecommerce.Catalog.Data.Models;
using Ecommerce.Catalog.Messaging;
using Ecommerce.Catalog.Messaging.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Catalog.Controllers {
    [Route("products")]
    public class ProductsController : ControllerBase {
        private readonly CatalogContext context;
        private readonly ICatalogClient catalogClient;

        public ProductsController(CatalogContext context, ICatalogClient catalogClient) {
            this.context = context;
            this.catalogClient = catalogClient;
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
            product = await context.Products.Include(x => x.Category).SingleAsync(x => x.Id == product.Id);
            await catalogClient.Publish(new CreatedNewProductEvent() { CategoryId = product.CategoryId, Id = product.Id, CategoryName = product.Category.Name, Name = product.Name });
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
