using Catalog.Domain.Interfaces;
using Catalog.Domain.Items;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Catalog.Api.Controllers
{
    [ApiController]
    [Route("/")]
    public class CatalogsController : ControllerBase
    {
        private readonly ILogger<CatalogsController> _logger;
        private readonly ICatalogRepository _catalogRepository;

        public CatalogsController(ILogger<CatalogsController> logger, ICatalogRepository catalogRepository)
        {
            _logger = logger;
            _catalogRepository = catalogRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public string Get()
        {
            return "Hello! Api started!!!";
        }

        [HttpGet("Categories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IEnumerable<Category> GetAllCategories()
        {
            return _catalogRepository.GetAllCategories();
        }
    }
}


