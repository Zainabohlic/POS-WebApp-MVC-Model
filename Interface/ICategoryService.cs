using POS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICategoryService
{
    Task<List<CategoryViewModel>> GetCategoriesWithProductsAsync();
    Task AddCategoryAsync(CategoryViewModel category);
    Task DeleteCategoryAsync(int id);

}
