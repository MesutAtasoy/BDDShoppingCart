namespace BDDShoppingCart.Specs.Models;

public class ShoppingCart
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal TotalPrices { get; set; }
    public List<ShoppingCartItem> ShoppingCartItems { get; set; } = new();
}