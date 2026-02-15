using System;
using System.Collections.Generic;

namespace SIADAL.Models;

public partial class teacher
{
    public ulong id { get; set; }

    public ulong user_id { get; set; }

    public int employee_number { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual ICollection<_class> _classes { get; set; } = new List<_class>();

    public virtual user user { get; set; } = null!;
}
