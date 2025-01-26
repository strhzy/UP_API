using System;
using System.Collections.Generic;

namespace API_UP.Models;

public partial class Order
{
    public long Id { get; set; }

    public long ClientId { get; set; }

    public DateTime DateReference { get; set; }

    public string Description { get; set; } = null!;

    public DateTime RepairDate { get; set; }

    public long StatusId { get; set; }

    public long ServiceStationId { get; set; }

    public decimal Price { get; set; }

    public long EmployeeId { get; set; }

    public long OperationId { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;

    public virtual Operation Operation { get; set; } = null!;

    public virtual ICollection<OrderSparePart> OrderSpareParts { get; set; } = new List<OrderSparePart>();

    public virtual ServiceStation ServiceStation { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;
}
