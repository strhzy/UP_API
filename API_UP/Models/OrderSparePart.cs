using System;
using System.Collections.Generic;

namespace API_UP.Models;

public partial class OrderSparePart
{
    public long Id { get; set; }

    public long SparePartId { get; set; }

    public long OrderId { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual SparePart SparePart { get; set; } = null!;
}
