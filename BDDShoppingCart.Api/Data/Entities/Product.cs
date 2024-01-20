namespace BDDShoppingCart.Api.Data.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string ProductCode  { get; set; }
    public string Name { get; set; }
    public decimal UnitPrice { get; set; }
}