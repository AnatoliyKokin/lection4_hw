using System;
using System.Collections.Generic;

namespace lection4_hw.DAL.Entities;

public partial class Deposit
{
    public long DepoNumber { get; set; }

    public long? Person { get; set; }

    public string? Currency { get; set; }

    public decimal? Balance { get; set; }

    public virtual Currency? CurrencyNavigation { get; set; }

    public virtual Person? PersonNavigation { get; set; }
}
