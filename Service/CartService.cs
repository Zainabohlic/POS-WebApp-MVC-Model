using System.Collections.Generic;
using System.Linq;
using POS.Models;
using POS.Service;

public class CartService : ICartService
{
    private readonly IProductService _productService;
    private static List<CartItemViewModel> _cart = new List<CartItemViewModel>();

    public CartService(IProductService productService)
    {
        _productService = productService;
    }

    public void AddToCart(int productId, int quantity)
    {
        // Fetch product details using injected ProductService
        var product = _productService.GetAllProductsAsync().Result.FirstOrDefault(p => p.ProductID == productId);
        if (product != null)
        {
            var cartItem = _cart.FirstOrDefault(c => c.ProductID == productId);
            if (cartItem == null)
            {
                _cart.Add(new CartItemViewModel
                {
                    ProductID = product.ProductID,
                    ProductName = product.ProductName,
                    Price = product.Price,
                    Quantity = quantity
                });
            }
            else
            {
                cartItem.Quantity += quantity;
            }
        }
    }

    public List<CartItemViewModel> GetCart()
    {
        return _cart;
    }
}
