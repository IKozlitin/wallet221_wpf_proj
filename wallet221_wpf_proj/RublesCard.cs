using System;
using System.Collections.Generic;

namespace wallet221_wpf_proj;

public partial class RublesCard
{
    public int Id { get; set; }

    public int? ClientId { get; set; }

    public string CardName { get; set; } = null!;

    public decimal CardBalance { get; set; }
}
