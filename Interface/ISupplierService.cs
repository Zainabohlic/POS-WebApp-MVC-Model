// ISupplierService.cs
using POS.Models;
using System.Collections.Generic;
using POS.Database;

public interface ISupplierService
{
    IEnumerable<Supplier> GetAllSuppliers();
}
