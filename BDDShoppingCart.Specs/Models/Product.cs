namespace BDDShoppingCart.Specs.Models;

public class Product
{
    public Guid Id { get; set; }
    public string ProductCode  { get; set; }
    public string Name { get; set; }
    public decimal UnitPrice { get; set; }
}