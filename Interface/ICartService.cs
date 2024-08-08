using System.Collections.Generic;
using POS.Models;

public interface ICartService
{
    void AddToCart(int productId, int quantity);
    List<CartItemViewModel> GetCart();
}
