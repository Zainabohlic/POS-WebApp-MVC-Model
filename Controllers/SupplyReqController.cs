// SupplyReqController.cs
using Microsoft.AspNetCore.Mvc;
using POS.Database;
using POS.Models;
using POS.Service;

using System.Security.Claims;

public class SupplyReqController : Controller
{
    private readonly ISupplyReqService _supplyReqService;
    private readonly IProductService _productService;
    private readonly ISupplierService _supplierService;

    public SupplyReqController(ISupplyReqService supplyReqService, IProductService productService, ISupplierService supplierService)
    {
        _supplyReqService = supplyReqService;
        _productService = productService;
        _supplierService = supplierService;
    }

    public IActionResult Create()
    {
        var suppliers = _supplierService.GetAllSuppliers();
        ViewBag.Suppliers = suppliers ?? new List<Supplier>(); 
        return View();
    }

   
    [HttpPost]
    public IActionResult Create(SupplyReqViewModel model)
    {
        Console.Write("ProductId: " + model.ProductID);
       // if (ModelState.IsValid)
        {
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            model.AddedBy = userId;
            model.AddedDate = DateTime.Now;

            // Debug here to check the ProductId
          

            _supplyReqService.CreateSupplyRequest(model);
            return RedirectToAction("Status", new { id = model.SupplyReqID });
        }

        // Ensure ViewBag.Suppliers is re-populated when validation fails
        ViewBag.Suppliers = _supplierService.GetAllSuppliers() ?? new List<Supplier>();
        return View(model);
    }



    public IActionResult Status()
    {
        var supplyRequests = _supplyReqService.GetAllSupplyRequests();
        return View(supplyRequests);
    }


    [HttpGet]
    public JsonResult GetProductsBySupplier(int supplierId)
    {
        var products = _productService.GetProductsBySupplier(supplierId);
        return Json(products);
    }
}
