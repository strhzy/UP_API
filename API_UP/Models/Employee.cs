using System;
using System.Collections.Generic;

namespace API_UP.Models;

public partial class Employee
{
    public long Id { get; set; }

    public string Surname { get; set; } = null!;

    public string EmployeeName { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public long PositionId { get; set; }

    public long QualificationId { get; set; }

    public long ServiceStationId { get; set; }

    public long EmployeeAccountId { get; set; }

    public virtual EmployeeAccount EmployeeAccount { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Position Position { get; set; } = null!;

    public virtual Qualification Qualification { get; set; } = null!;

    public virtual ServiceStation ServiceStation { get; set; } = null!;
}
