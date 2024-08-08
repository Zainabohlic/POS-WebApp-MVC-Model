using System;
using System.Collections.Generic;

namespace POS.Database;

public partial class SupplyReq
{
    public int SupplyReqId { get; set; }

    public int? ProductId { get; set; }

    public int? SupplierId { get; set; }

    public int Quantity { get; set; }

    public string? Status { get; set; }

    public int? AddedBy { get; set; }

    public DateTime? AddedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedTime { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedTime { get; set; }
}
