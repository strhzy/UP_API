using System;
using System.Collections.Generic;

namespace API_UP.Models;

public partial class EmployeeAccount
{
    public long Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public long? RoleId { get; set; }
    
    public string Telephone { get; set; }
    
    public string Email { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual Role? Role { get; set; }
}
