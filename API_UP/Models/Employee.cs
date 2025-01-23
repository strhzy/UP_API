using System;
using System.Collections.Generic;

namespace API_UP.Models;

public partial class Employee
{
    public long Id { get; set; }

    public string Surname { get; set; } = null!;

    public string EmployeeName { get; set; } = null!;

    public string? Patronymic { get; set; }

    public long? PositionId { get; set; }

    public long? QualificationId { get; set; }

    public long? ServiceStationId { get; set; }

    public long? EmployeeAccountId { get; set; }

    public virtual EmployeeAccount? EmployeeAccount { get; set; }

    public virtual Position? Position { get; set; }

    public virtual Qualification? Qualification { get; set; }

    public virtual ServiceStation? ServiceStation { get; set; }
}
