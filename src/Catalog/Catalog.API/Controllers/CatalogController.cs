using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Catalog.API.Entities;
using Catalog.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly Logger<CatalogController> _logger;

        public CatalogController(IProductRepository productRepository, Logger<CatalogController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productRepository.GetProducts();
            return Ok(products);
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(Product),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Product>> GetProduct(string id)
        {
            var product = await _productRepository.GetProduct(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>>  GetProductByCategoryName(string categoryName)
        {
            var products = await _productRepository.GetProductsByCategory(categoryName);
            return Ok(products);
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>>  GetProductByName(string name)
        {
            var products = await _productRepository.GetProductsByName(name);
            return Ok(products);
        }
        
        [HttpPost]
        public async Task<ActionResult> Create([FromBody]Product product)
        {
            await _productRepository.Create(product);
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult<bool>> Update(Product product)
        {
            var updateResult = await _productRepository.Update(product);
            return Ok(updateResult);
        }
        
        [HttpDelete]
        public async Task<ActionResult<bool>> Delete(string id)
        {
            var deleteResult = await _productRepository.Delete(id);
            return Ok(deleteResult);
        }
    }
}
