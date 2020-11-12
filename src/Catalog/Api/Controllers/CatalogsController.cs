using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Catalog.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogsController : ControllerBase
    {
        private readonly ILogger<CatalogsController> _logger;

        public CatalogsController(ILogger<CatalogsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public string Get()
        {
            return "Hello! Api started!!!";
        }
    }
}


