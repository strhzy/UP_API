using System;
using System.Collections.Generic;

namespace API_UP.Models;

public partial class Position
{
    public int Id { get; set; }

    public string PositionName { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
