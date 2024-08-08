using Microsoft.EntityFrameworkCore;
using POS.Database;
using POS.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CategoryService : ICategoryService
{
    private readonly POS_db_Context _context;

    public CategoryService(POS_db_Context context)
    {
        _context = context;
    }

    public async Task<List<CategoryViewModel>> GetCategoriesWithProductsAsync()
    {
        return await (from c in _context.Categories
                      select new CategoryViewModel
                      {
                          CategoryID = c.CategoryId,
                          CategoryName = c.CategoryName,
                      }).ToListAsync();
    }

    public async Task AddCategoryAsync(CategoryViewModel category)
    {
        var newCategory = new Category
        {
            CategoryName = category.CategoryName
        };

        _context.Categories.Add(newCategory);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCategoryAsync(int categoryId)
    {
        var category = await _context.Categories.FindAsync(categoryId);
        if (category != null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
