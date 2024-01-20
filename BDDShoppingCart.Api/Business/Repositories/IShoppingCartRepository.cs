using BDDShoppingCart.Api.Data.Entities;

namespace BDDShoppingCart.Api.Business.Repositories;

public interface IShoppingCartRepository
{
    Task<ShoppingCart?> CreateCartAsync(Guid userId);
    Task<ShoppingCart?> GetShoppingCartByIdAsync(Guid id);
    Task<ShoppingCartItem> AddProductAsync(ShoppingCart cart, Product product, int quantity);
}