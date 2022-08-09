using Ecommerce.Catalog.Data;
using Ecommerce.Catalog.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Catalog.Controllers {
    [Route("categories")]
    public class CategoriesController : ControllerBase {
        private readonly CatalogContext context;

        public CategoriesController(CatalogContext context) {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Category>>> Index() {
            return await context.Categories.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> Get(int id) {
            var prod = await context.Categories.FindAsync(id);

            if (prod == null) {
                return NotFound();
            }

            return prod;
        }

        [HttpPost]
        public async Task<ActionResult<Category>> Post([FromBody] Category category) {
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
            return category;
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            var category = await context.Categories.FindAsync(id);

            if (category == null) {
                return NotFound();
            }

            context.Categories.Remove(category);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
