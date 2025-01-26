using System;
using System.Collections.Generic;

namespace API_UP.Models;

public partial class Operation
{
    public long Id { get; set; }

    public string OperationName { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
