using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using POS.Models;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

[Authorize]
public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
       

    }

    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAllProductsAsync();
        return View(products);
    }

    public async Task<IActionResult> Add()
    {
        ViewBag.Categories = await _productService.GetAllCategoriesAsync();
        ViewBag.Suppliers = await _productService.GetAllSuppliersAsync();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(ProductViewModel model)
    {
        if (ModelState.IsValid)
        {
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            if (userId == 0)
            {
                ModelState.AddModelError("", "User ID not found.");
                return View(model);
            }

           
            model.AddedBy = userId;
            model.AddedDate = DateTime.Now;
            await _productService.AddProductAsync(model);
            return RedirectToAction("Index");
        }
        return View(model);
    }


    public async Task<IActionResult> Edit(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        ViewBag.Categories = await _productService.GetAllCategoriesAsync();
        ViewBag.Suppliers = await _productService.GetAllSuppliersAsync();
        return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ProductViewModel model)
    {
        if (ModelState.IsValid)
        {
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            model.UpdatedBy  = userId;
            model.UpdatedTime = DateTime.Now;
            await _productService.UpdateProductAsync(model);
            return RedirectToAction("Index");
        }
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _productService.DeleteProductAsync(id);
        return RedirectToAction("Index");
    }

   
}

