﻿using System;
using System.Collections.Generic;

namespace API_UP.Models;

public partial class SparePart
{
    public long Id { get; set; }

    public string PartName { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual ICollection<OrderSparePart> OrderSpareParts { get; set; } = new List<OrderSparePart>();
}
