using BDDShoppingCart.Api.Business.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BDDShoppingCart.Api.Controller;

[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var products = await _productRepository.GetProductsAsync();
        return Ok(products);
    }

    [HttpGet("{code}")]
    public async Task<ActionResult> GetByCode(string code)
    {
        var product = await _productRepository.GetProductByCodeAsync(code);
        if (product == null)
        {
            return NotFound();
        }
        
        return Ok(product);
    }
}