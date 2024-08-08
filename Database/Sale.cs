using System;
using System.Collections.Generic;

namespace POS.Database;

public partial class Sale
{
    public int SaleId { get; set; }

    public int? CustomerId { get; set; }

    public decimal TotalAmount { get; set; }

    public int? AddedBy { get; set; }

    public DateTime? AddedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedTime { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedTime { get; set; }
}
