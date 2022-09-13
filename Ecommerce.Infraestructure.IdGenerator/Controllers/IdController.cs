using Ecommerce.Infraestructure.IdGenerator.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Infraestructure.IdGenerator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdController : ControllerBase
    {
        private readonly ILogger<IdController> _logger;
        private readonly IIdGeneratorService _generatorService;

        public IdController(ILogger<IdController> logger, IIdGeneratorService generatorService)
        {
            _logger = logger;
            _generatorService = generatorService;
        }

        [HttpGet("new-id")]
        public IActionResult Get()
        {
            var id = _generatorService.Generate();
            _logger.LogInformation("Generated Id: {Id} for Request: {Request}", id, Request.HttpContext.TraceIdentifier);
            return Ok(id);
        }

        [HttpGet("bulk-create")]
        public IActionResult Get([FromQuery] int count)
        {
            var ids = _generatorService.Generate(count);
            _logger.LogInformation("Generated {Count} Ids for Request: {Request}", ids.Count, Request.HttpContext.TraceIdentifier);
            return Ok(ids);
        }
    }
}