using System;
using System.Collections.Generic;

namespace wallet221_wpf_proj;

public partial class ExchangeRateList
{
    public int Id { get; set; }

    public string CurrencyName { get; set; } = null!;

    public double CurrencyRate { get; set; }
}
