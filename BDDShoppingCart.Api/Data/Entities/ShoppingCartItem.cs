namespace BDDShoppingCart.Api.Data.Entities;

public class ShoppingCartItem
{
    public Guid Id { get; set; }
    
    public Guid ShoppingCartId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    
    public virtual ShoppingCart ShoppingCart { get; set; }
}