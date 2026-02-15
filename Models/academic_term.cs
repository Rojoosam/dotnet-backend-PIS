using System;
using System.Collections.Generic;

namespace SIADAL.Models;

public partial class academic_term
{
    public ulong id { get; set; }

    public string name { get; set; } = null!;

    public DateOnly start_date { get; set; }

    public DateOnly end_date { get; set; }

    public bool? is_active { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual ICollection<_class> _classes { get; set; } = new List<_class>();

    public virtual ICollection<group> groups { get; set; } = new List<group>();
}
