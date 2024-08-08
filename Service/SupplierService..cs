// SupplierService.cs
using POS.Models;

using System.Collections.Generic;
using System.Linq;
using POS.Database;

public class SupplierService : ISupplierService
{
    private readonly POS_db_Context _context;

    public SupplierService(POS_db_Context context)
    {
        _context = context;
    }

    public IEnumerable<Supplier> GetAllSuppliers()
    {
        return _context.Suppliers.ToList();
    }
}
namespace POS.Service
{
    public class SupplierService
    {
    }
}
