using System;
using System.Collections.Generic;

namespace POS.Database;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public string? ContactNumber { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public int? AddedBy { get; set; }

    public DateTime? AddedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedTime { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedTime { get; set; }
}
