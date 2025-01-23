using System;
using System.Collections.Generic;

namespace API_UP.Models;

public partial class ServiceStation
{
    public long Id { get; set; }

    public string Address { get; set; } = null!;

    public string TelephoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public long QuantityWorkPlaces { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
