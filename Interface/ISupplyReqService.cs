using POS.Models;
using System.Collections.Generic;

public interface ISupplyReqService
{
    void CreateSupplyRequest(SupplyReqViewModel model);
    string GetRequestStatus(int id);
    IEnumerable<SupplyReqViewModel> GetAllSupplyRequests(); // Add this method
}
