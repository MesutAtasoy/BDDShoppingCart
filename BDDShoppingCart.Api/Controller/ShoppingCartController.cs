using BDDShoppingCart.Api.Business.Dto;
using BDDShoppingCart.Api.Business.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BDDShoppingCart.Api.Controller;

[Route("api/shoppingcarts")]
public class ShoppingCartController : ControllerBase
{
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IProductRepository _productRepository;

    public ShoppingCartController(IShoppingCartRepository shoppingCartRepository,
        IProductRepository productRepository)
    {
        _shoppingCartRepository = shoppingCartRepository;
        _productRepository = productRepository;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(Guid id)
    {
        var shoppingCart = await _shoppingCartRepository.GetShoppingCartByIdAsync(id);
        
        if (shoppingCart == null)
        {
            return NotFound("Cart is not found");
        }
        
        return Ok(shoppingCart);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateShoppingCartDto request)
    {
        var shoppingCart = await _shoppingCartRepository.CreateCartAsync(request.UserId);
        
        return CreatedAtAction(nameof(GetById), new { id = shoppingCart.Id},  shoppingCart);
    }

    [HttpPost("{cartId}/products/{productId}/add/{quantity}")]
    public async Task<ActionResult> AddProductAsync(Guid cartId, Guid productId, int quantity)
    {
        var shoppingCart = await _shoppingCartRepository.GetShoppingCartByIdAsync(cartId);
        if (shoppingCart == null)
        {
            return NotFound("Cart is not found");
        }

        var product = await _productRepository.GetProductByIdAsync(productId);
        if (product == null)
        {
            return NotFound("Product is not found");
        }

        await _shoppingCartRepository.AddProductAsync(shoppingCart, product, quantity);

        return NoContent();
    }
}