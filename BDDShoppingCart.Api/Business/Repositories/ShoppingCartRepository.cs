using BDDShoppingCart.Api.Data;
using BDDShoppingCart.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BDDShoppingCart.Api.Business.Repositories;

public class ShoppingCartRepository : IShoppingCartRepository
{
    private readonly BddShoppingCartDbContext _shoppingCartDbContext;

    public ShoppingCartRepository(BddShoppingCartDbContext shoppingCartDbContext)
    {
        _shoppingCartDbContext = shoppingCartDbContext;
    }

    public async Task<ShoppingCart?> GetShoppingCartByIdAsync(Guid id)
    {
        return await _shoppingCartDbContext.ShoppingCarts
            .Include(x => x.ShoppingCartItems)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ShoppingCart?> CreateCartAsync(Guid userId)
    {
        var shoppingCard = new ShoppingCart { Id = Guid.NewGuid(), UserId = userId };

        await _shoppingCartDbContext.ShoppingCarts.AddAsync(shoppingCard);

        await _shoppingCartDbContext.SaveChangesAsync();

        return shoppingCard;
    }

    public async Task<ShoppingCartItem> AddProductAsync(ShoppingCart cart, Product product, int quantity)
    {
        var price = product.UnitPrice * quantity;

        var item = new ShoppingCartItem
        {
            Price = price,
            Quantity = quantity,
            ProductId = product.Id
        };

        cart.TotalPrices += price;
        cart.ShoppingCartItems.Add(item);

        await _shoppingCartDbContext.SaveChangesAsync();

        return item;
    }
}