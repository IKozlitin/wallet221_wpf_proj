using System;
using System.Collections.Generic;

namespace wallet221_wpf_proj;

public partial class RublesDeposit
{
    public int Id { get; set; }

    public int? ClientId { get; set; }

    public string DepositName { get; set; } = null!;

    public decimal DepositBalance { get; set; }

    public double DepositPercent { get; set; }
}
