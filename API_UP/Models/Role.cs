using System;
using System.Collections.Generic;

namespace API_UP.Models;

public partial class Role
{
    public long Id { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<EmployeeAccount> EmployeeAccounts { get; set; } = new List<EmployeeAccount>();
}
