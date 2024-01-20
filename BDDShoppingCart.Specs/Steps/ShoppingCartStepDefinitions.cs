using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using BDDShoppingCart.Specs.Extensions;
using BDDShoppingCart.Specs.Models;
using FluentAssertions;
using Microsoft.Extensions.Configuration;

namespace BDDShoppingCart.Specs.Steps;

[Binding]
public class ShoppingCartStepDefinitions
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    
    private ShoppingCart _shoppingCart = new();
    private Product _addedProduct = new(); 

    public ShoppingCartStepDefinitions(IConfiguration configuration)
    {
        _configuration = configuration;
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(_configuration["BBDShoppingCart:BaseUri"]);
    }

    [Given(@"an empty shopping cart")]
    public async Task GivenAEmptyCart()
    {
        var createShoppingCart = new { UserId = Guid.NewGuid() };
        
        var content = new StringContent(JsonSerializer.Serialize(createShoppingCart), Encoding.UTF8, MediaTypeNames.Application.Json);

        var response = await _httpClient.PostAsync("/api/shoppingcarts", content);

        response.Should().NotBeNull();
        response.IsSuccessStatusCode.Should().BeTrue();
        
        var shoppingCart = await response.Content.ReadFromJsonAsync<ShoppingCart>();

        shoppingCart.Should().NotBeNull();

        _shoppingCart = shoppingCart;
    }
    
    
    [When(@"I add a product randomly to the shopping cart.")]
    public async Task WhenIAddAProductRandomlyToTheShoppingCart()
    {
        var products = await _httpClient.GetFromJsonAsync<List<Product>>("/api/products");

        products.Should().NotBeNull();
        products.Count.Should().BeGreaterThan(0);

        var randomProduct = products.RandomElement();
        
        var cartResponse = await _httpClient.PostAsync($"/api/shoppingcarts/{_shoppingCart.Id}/products/{randomProduct?.Id}/add/1", null);

        cartResponse.Should().NotBeNull();
        cartResponse.IsSuccessStatusCode.Should().BeTrue();

        _addedProduct = randomProduct;
    }


    [Then(@"The price of the shopping cart is a valid number.")]
    public async Task ThenTheShoppingCartPriceIs()
    {
        var shoppingCart = await _httpClient.GetFromJsonAsync<ShoppingCart>($"api/shoppingcarts/{_shoppingCart.Id}");
      
        shoppingCart.Should().NotBeNull();
        shoppingCart.TotalPrices.Should().BeGreaterThan(0);
    }

    [Then(@"the added item should be in the shopping cart")]
    public async Task ThenTheAddedItemShouldBeInTheShoppingCart()
    {
        var shoppingCart = await _httpClient.GetFromJsonAsync<ShoppingCart>($"api/shoppingcarts/{_shoppingCart.Id}");
        
        shoppingCart.Should().NotBeNull();
        shoppingCart.ShoppingCartItems.Should().Contain(x => x.ProductId == _addedProduct.Id);
    }
    
    [When(@"I add (.*) item\(s\) with the product code ""(.*)"" to my shopping cart.")]
    public async Task WhenIAddItemSWithTheProductCodeToMyShoppingCart(int quantity, string productCode)
    {
        var product = await _httpClient.GetFromJsonAsync<Product>($"api/products/{productCode}");
        product.Should().NotBeNull();
        
        var cartResponse = await _httpClient.PostAsync($"/api/shoppingcarts/{_shoppingCart.Id}/products/{product?.Id}/add/{quantity}", null);

        cartResponse.Should().NotBeNull();
        cartResponse.IsSuccessStatusCode.Should().BeTrue();
    }

    [Then(@"The shopping cart costs (.*)")]
    public async Task ThenTheShoppingCartCosts(int price)
    {
        var shoppingCart = await _httpClient.GetFromJsonAsync<ShoppingCart>($"api/shoppingcarts/{_shoppingCart.Id}");
      
        shoppingCart.Should().NotBeNull();
        shoppingCart.TotalPrices.Should().Be(price);
    }
}