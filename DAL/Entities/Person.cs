using System;
using System.Collections.Generic;

namespace lection4_hw.DAL.Entities;

public partial class Person
{
    public long Passport { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public virtual ICollection<Deposit> Deposits { get; } = new List<Deposit>();
}
