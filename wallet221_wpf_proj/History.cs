using System;
using System.Collections.Generic;

namespace wallet221_wpf_proj;

public partial class History
{
    public int Id { get; set; }

    public int? ClientId { get; set; }

    public string? Operation { get; set; }

    public DateTime CreateAt { get; set; }

    public virtual Client? Client { get; set; }
}
