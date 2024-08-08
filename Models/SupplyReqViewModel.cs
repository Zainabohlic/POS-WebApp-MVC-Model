namespace POS.Models
{
    public class SupplyReqViewModel
    {
        public int SupplyReqID { get; set; }
        public int ProductID { get; set; }
        public int SupplierID { get; set; }
        public int Quantity { get; set; }
        public DateTime AddedDate { get; set; }
        public int AddedBy { get; set; }
        public string Status { get; set; } // Include Status property
    }

    public class SupplyReqStatusViewModel
    {
        public List<SupplyReqViewModel> SupplyReqs { get; set; }
        public List<ProductViewModel> Products { get; set; }
        public List<SupplierViewModel> Suppliers { get; set; }
    }
}
