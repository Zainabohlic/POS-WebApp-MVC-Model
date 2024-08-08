using Microsoft.AspNetCore.Mvc;
using POS.Models;
using POS.Service;
using System.Threading.Tasks;

public class CartController : Controller
{
    private readonly IProductService _productService;
    private readonly ICartService _cartService;

    public CartController(IProductService productService, ICartService cartService)
    {
        _productService = productService;
        _cartService = cartService;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAllProductsAsync();
        return View(products);
    }

    [HttpPost]
    public IActionResult AddToCart(int productId, int quantity)
    {
        _cartService.AddToCart(productId, quantity);
        return RedirectToAction("Cart");
    }

    public IActionResult Cart()
    {
        var cart = _cartService.GetCart();
        return View(cart);
    }
}
