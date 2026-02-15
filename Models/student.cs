using System;
using System.Collections.Generic;

namespace SIADAL.Models;

public partial class student
{
    public ulong id { get; set; }

    public ulong user_id { get; set; }

    public int enrollment_number { get; set; }

    public DateOnly birth_date { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual ICollection<activity> activities { get; set; } = new List<activity>();

    public virtual user user { get; set; } = null!;
}
