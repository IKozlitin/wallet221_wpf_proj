using System;
using System.Collections.Generic;

namespace wallet221_wpf_proj;

public partial class Client
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string SurName { get; set; } = null!;

    public long PhoneNumber { get; set; }

    public virtual ICollection<History> Histories { get; set; } = new List<History>();

    public virtual ICollection<RublesCard> RublesCards { get; set; } = new List<RublesCard>();

    public virtual ICollection<RublesDeposit> RublesDeposits { get; set; } = new List<RublesDeposit>();
}
