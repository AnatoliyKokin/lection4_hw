using System;
using System.Collections.Generic;

namespace lection4_hw.DAL.Entities;

public partial class Currency
{
    public string ShortTitle { get; set; } = null!;

    public string? LongTitle { get; set; }

    public string? Country { get; set; }

    public virtual ICollection<Deposit> Deposits { get; } = new List<Deposit>();
}
