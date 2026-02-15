using System;
using System.Collections.Generic;

namespace SIADAL.Models;

public partial class group_class
{
    public ulong id { get; set; }

    public ulong group_id { get; set; }

    public ulong class_id { get; set; }

    public DateTime? created_at { get; set; }

    public DateTime? updated_at { get; set; }

    public virtual _class _class { get; set; } = null!;

    public virtual group group { get; set; } = null!;
}
