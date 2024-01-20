using BDDShoppingCart.Api.Data;
using BDDShoppingCart.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BDDShoppingCart.Api.Business.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly BddShoppingCartDbContext _shoppingCartDbContext;

    public ProductRepository(BddShoppingCartDbContext shoppingCartDbContext)
    {
        _shoppingCartDbContext = shoppingCartDbContext;
    }

    public async Task<List<Product?>> GetProductsAsync()
    {
        return await _shoppingCartDbContext.Products.ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(Guid id)
    {
        return await _shoppingCartDbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Product?> GetProductByCodeAsync(string productCode)
    {
        return await _shoppingCartDbContext.Products.FirstOrDefaultAsync(x => x.ProductCode == productCode);
    }
}