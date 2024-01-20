using BDDShoppingCart.Api.Data.Entities;

namespace BDDShoppingCart.Api.Business.Repositories;

public interface IProductRepository
{
    Task<List<Product?>> GetProductsAsync();
    Task<Product?> GetProductByIdAsync(Guid id);
    Task<Product?> GetProductByCodeAsync(string productCode);
}