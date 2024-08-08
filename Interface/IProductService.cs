using POS.Database;
using POS.Models;

public interface IProductService
{
    Task<List<ProductViewModel>> GetAllProductsAsync();
    Task<ProductViewModel> GetProductByIdAsync(int productId);
    Task AddProductAsync(ProductViewModel product);
    Task UpdateProductAsync(ProductViewModel product);
    Task DeleteProductAsync(int productId);
    IEnumerable<Product> GetProductsBySupplier(int supplierId);
    Task<List<CategoryViewModel>> GetAllCategoriesAsync();
    Task<List<SupplierViewModel>> GetAllSuppliersAsync();
    //Task<List<ProductViewModel>> GetProductsBySupplierAsync(int supplierId);  // Add this line

}
