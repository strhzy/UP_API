using System;
using System.Collections.Generic;

namespace API_UP.Models;

public partial class Order
{
    public long Id { get; set; }

    public long? ClientId { get; set; }

    public DateTime DateReference { get; set; }

    public DateTime? RepairDate { get; set; }

    public string? StatusName { get; set; }

    public virtual Client? Client { get; set; }

    public virtual ICollection<OrderSparePart> OrderSpareParts { get; set; } = new List<OrderSparePart>();
}
