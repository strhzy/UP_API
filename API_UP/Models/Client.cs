using System;
using System.Collections.Generic;

namespace API_UP.Models;

public partial class Client
{
    public long Id { get; set; }

    public string Surname { get; set; } = null!;

    public string ClientName { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public string TelephoneNumber { get; set; } = null!;

    public string CarBrand { get; set; } = null!;

    public string CarModel { get; set; } = null!;

    public string GovNumber { get; set; } = null!;

    public DateOnly LastVisitDate { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
