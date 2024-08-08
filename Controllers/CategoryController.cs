using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using POS.Models;

[Authorize]
public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var categories = await _categoryService.GetCategoriesWithProductsAsync();
            return View(categories);
        }
        catch (Exception ex)
        {
            return View();
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddCategory(CategoryViewModel model)
    {
        if (ModelState.IsValid)
        {
            await _categoryService.AddCategoryAsync(model);
            return Json(new { success = true });
        }
        return Json(new { success = false });
    }

    [HttpPost]
    public async Task<IActionResult> DeleteCategory(int categoryId)
    {
        try
        {
            await _categoryService.DeleteCategoryAsync(categoryId);
            return Json(new { success = true });
        }
        catch
        {
            return Json(new { success = false });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        try
        {
            var categories = await _categoryService.GetCategoriesWithProductsAsync();
            return PartialView("_CategoriesPartial", categories);
        }
        catch
        {
            return PartialView("_CategoriesPartial", new List<CategoryViewModel>());
        }
    }
}
