using POS.Models;
using POS.Database;
using System.Collections.Generic;
using System.Linq;

public class SupplyReqService : ISupplyReqService
{
    private readonly POS_db_Context _context;

    public SupplyReqService(POS_db_Context context)
    {
        _context = context;
    }

    public void CreateSupplyRequest(SupplyReqViewModel model)
    {
        var supplyReq = new SupplyReq
        {
            ProductId = model.ProductID,
            SupplierId = model.SupplierID,
            Quantity = model.Quantity,
            Status = "Pending",
            AddedBy = model.AddedBy,
            AddedDate = model.AddedDate
        };
        _context.SupplyReqs.Add(supplyReq);
        _context.SaveChanges();
    }

    public string GetRequestStatus(int id)
    {
        var request = _context.SupplyReqs.Find(id);
        return request?.Status;
    }

    public IEnumerable<SupplyReqViewModel> GetAllSupplyRequests()
    {
        return _context.SupplyReqs.Select(s => new SupplyReqViewModel
        {
            SupplyReqID = s.SupplyReqId,
            ProductID = (int)s.ProductId,
            SupplierID =(int) s.SupplierId,
            Quantity = s.Quantity,
            Status = s.Status,
            AddedBy = (int)s.AddedBy,
            //AddedDate = (DateTime)s.AddedDate
        }).ToList();
    }
}
