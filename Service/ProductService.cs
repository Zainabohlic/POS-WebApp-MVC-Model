using Microsoft.EntityFrameworkCore;
using POS.Database;
using POS.Models;


public class ProductService : IProductService
{
    private readonly POS_db_Context _context;

    public ProductService(POS_db_Context context)
    {
        _context = context;
    }

    public async Task<List<ProductViewModel>> GetAllProductsAsync()
    {
        var product=await _context.Products.FirstOrDefaultAsync();

        var products = await _context.Products.ToListAsync();

        return await _context.Products.Select(p => new ProductViewModel
        {
            ProductID = p.ProductId,
            ProductName = p.ProductName,
            Price = p.Price,
            StockQuantity = p.StockQuantity,
            SupplierID = p.CategoryId,
            CategoryID = p.CategoryId,
            AddedBy = (int)p.AddedBy,
            AddedDate = p.AddedDate,
            UpdatedBy = p.UpdatedBy,
            UpdatedTime = p.UpdatedTime,
            DeletedBy = p.DeletedBy,
            DeletedTime = p.DeletedTime
        }).ToListAsync();
    }

    public async Task<ProductViewModel> GetProductByIdAsync(int productId)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product == null)
        {
            return null;
        }

        return new ProductViewModel
        {
            ProductID = product.ProductId,
            ProductName = product.ProductName,
            Price = product.Price,
            StockQuantity = product.StockQuantity,
            SupplierID = product.SupplierId,
            CategoryID = product.CategoryId,
            AddedBy = (int)product.AddedBy,
            AddedDate = product.AddedDate,
            UpdatedBy = product.UpdatedBy,
            UpdatedTime = product.UpdatedTime,
            DeletedBy = product.DeletedBy,
            DeletedTime = product.DeletedTime
        };
    }

    public async Task AddProductAsync(ProductViewModel product)
    {
        var newProduct = new Product
        {
            ProductName = product.ProductName,
            Price = product.Price,
            StockQuantity = product.StockQuantity,
            SupplierId = product.SupplierID,
            CategoryId = product.CategoryID,
            AddedBy = product.AddedBy,
            AddedDate = product.AddedDate,
            UpdatedBy = product.UpdatedBy,
            UpdatedTime = product.UpdatedTime,
            DeletedBy = product.DeletedBy,
            DeletedTime = product.DeletedTime
        };

        _context.Products.Add(newProduct);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateProductAsync(ProductViewModel product)
    {
        var existingProduct = await _context.Products.FindAsync(product.ProductID);
        if (existingProduct != null)
        {
            existingProduct.ProductName = product.ProductName;
            existingProduct.Price = product.Price;
            existingProduct.StockQuantity = product.StockQuantity;
            existingProduct.SupplierId = product.SupplierID;
            existingProduct.CategoryId = product.CategoryID;
            existingProduct.UpdatedBy = product.UpdatedBy;
            existingProduct.UpdatedTime = product.UpdatedTime;

            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteProductAsync(int productId)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<CategoryViewModel>> GetAllCategoriesAsync()
    {
        return await _context.Categories.Select(c => new CategoryViewModel
        {
            CategoryID = c.CategoryId,
            CategoryName = c.CategoryName
        }).ToListAsync();
    }

    public async Task<List<SupplierViewModel>> GetAllSuppliersAsync()
    {
        return await _context.Suppliers.Select(s => new SupplierViewModel
        {
            SupplierID = s.SupplierId,
            SupplierName = s.SupplierName
        }).ToListAsync();
    }

    //public async Task<List<ProductViewModel>> GetProductsBySupplierAsync(int supplierId)
    //{
    //    return await _context.Products
    //        .Where(p => p.SupplierId == supplierId)
    //        .Select(p => new ProductViewModel
    //        {
    //            ProductID = p.ProductId,
    //            ProductName = p.ProductName
    //        })
    //        .ToListAsync();
    //}

    public IEnumerable<Product> GetProductsBySupplier(int supplierId)
    {
        return _context.Products.Where(p => p.SupplierId == supplierId).ToList();
    }
}
